using Hangfire;
using Microsoft.Extensions.Logging;
using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.CanhBao;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Uow;

namespace quanLyDuAnNoiBo.Workers
{
    public class CanhBaoWorker : HangfireBackgroundWorkerBase
    {
        private readonly IAbpDistributedLock _distributedLock;
        private readonly string SERVICE_NAME = "<CanhBaoService>";
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ICanhBaoRepository _canhBaoRepository;
        private readonly IModuleDuAnRepository _moduleDuAnRepository;
        private readonly IAccountRepository _accountRepository;
        public CanhBaoWorker(
            IAbpDistributedLock distributedLock,
            IUnitOfWorkManager unitOfWorkManager,
            ISubTaskRepository subTaskRepository,
            ICanhBaoRepository canhBaoRepository,
            IModuleDuAnRepository moduleDuAnRepository,
            IAccountRepository accountRepository
            )
        {
            RecurringJobId = nameof(CanhBaoWorker);
            CronExpression = Cron.Daily(9); // run at 9:00 AM every day
            //CronExpression = Cron.MinuteInterval(1); // run every minute
            TimeZone = TimeZoneInfo.Local;
            _distributedLock = distributedLock;
            _unitOfWorkManager = unitOfWorkManager;
            _subTaskRepository = subTaskRepository;
            _canhBaoRepository = canhBaoRepository;
            _moduleDuAnRepository = moduleDuAnRepository;
            _accountRepository = accountRepository;
        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            string jobName = "CanhBaoWorker";
            await using (var handle = await _distributedLock.TryAcquireAsync("CanhBaoWorker"))
            {
                // wait for previous db transaction to complete
                Thread.Sleep(1000);
                if (handle == null)
                {
                    Logger.LogWarning("{@serviceName} - {@jobName} Cannot acquire distributed lock", SERVICE_NAME, jobName);
                    return;
                }

                try
                {
                    using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false))
                    {
                        var admin = await _accountRepository.GetAdminDefault();
                        var subtasks = await _subTaskRepository.GetListAsync();
                        var tasks = await _moduleDuAnRepository.GetListAsync();
                        foreach (var subtask in subtasks)
                        {
                            var canhBao = await _canhBaoRepository.GetCanhBaoBySubtaskId(subtask.Id);
                            if (canhBao != null)
                            {
                                canhBao.UpdateCanhBao(subtask.ThoiGianKetThuc, subtask.TrangThaiSubtask);
                            }
                        }
                        await uow.CompleteAsync();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
