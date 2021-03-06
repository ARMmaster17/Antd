﻿///-------------------------------------------------------------------------------------
///     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
///     All rights reserved.
///
///     Redistribution and use in source and binary forms, with or without
///     modification, are permitted provided that the following conditions are met:
///         * Redistributions of source code must retain the above copyright
///           notice, this list of conditions and the following disclaimer.
///         * Redistributions in binary form must reproduce the above copyright
///           notice, this list of conditions and the following disclaimer in the
///           documentation and/or other materials provided with the distribution.
///         * Neither the name of the Anthilla S.r.l. nor the
///           names of its contributors may be used to endorse or promote products
///           derived from this software without specific prior written permission.
///
///     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
///     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
///     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
///     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
///     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
///     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
///     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
///     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
///     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///
///     20141110
///-------------------------------------------------------------------------------------

using Newtonsoft.Json;
using Antd.Common;
using System;
using System.Threading;
using Quartz;

namespace Antd.Scheduler {

    public class Job {

        public static void Schedule(string jobName, string command) {
            var guid = Guid.NewGuid().ToString();
            string[] data = new string[] {
                    command.GetFirstString(),
                    command.GetAllStringsButFirst()
                };
            string dataJson = JsonConvert.SerializeObject(data);
            var job = JobRepository.SetTaskOneTimeOnly(guid, jobName, dataJson);
            Thread.Sleep(20);
            JobScheduler.LaunchJob<JobList.CommandJob>(guid);
        }

        public static void Schedule(string jobName, string command, string cron) {
            var guid = Guid.NewGuid().ToString();
            string[] data = new string[] {
                    command.GetFirstString(),
                    command.GetAllStringsButFirst()
                };
            string dataJson = JsonConvert.SerializeObject(data);
            var job = JobRepository.SetTaskCron(guid, jobName, dataJson, cron);
            Thread.Sleep(20);
            JobScheduler.LaunchJob<JobList.CommandJob>(guid);
        }

        public static void ReSchedule(string guid) {
            var task = JobRepository.GetByGuid(guid);
            var job = JobRepository.SetTaskOneTimeOnly(guid, task.Alias, task.Data);
            JobScheduler.LaunchJob<JobList.CommandJob>(guid);
        }
    }
}