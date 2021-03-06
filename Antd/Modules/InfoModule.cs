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

using Antd.Status;
using Nancy;
using System.Dynamic;

namespace Antd.Modules {

    public class InfoModule : NancyModule {

        public InfoModule()
            : base("/info") {
            Get["/"] = x => {
                dynamic vmod = new ExpandoObject();
                vmod.hostname = Command.Launch("hostname", "").output;
                vmod.os = Version.GetModel().value;
                vmod.time = Command.Launch("date", "").output;
                vmod.procinfo = "";
                vmod.uptime = Uptime.UpTime;
                vmod.runprocs = Proc.All.ToArray().Length.ToString();
                vmod.CPUload = Uptime.LoadAverage;
                vmod.CPUusage = "";
                vmod.rmem = "";
                vmod.ldskspc = "";
                vmod.MEMINFO = Meminfo.GetModel();
                vmod.CPUINFO = Cpuinfo.GetModel();
                vmod.VERSION = Version.GetModel();
                vmod.DMIDECODE = Command.Launch("dmidecode", "");
                vmod.IFCONFIG = Command.Launch("ifconfig", "");
                vmod.PROCS = Proc.All;
                return View["_page-info", vmod];
            };

            Get["/loadaverage"] = x => {
                return Response.AsJson(Uptime.LoadAverage);
            };

            Get["/disk"] = x => {
                return Response.AsJson(Uptime.LoadAverage);
            };

            Post["/killproc"] = x => {
                string pid = (string)this.Request.Form.data;
                CommandModel command = Command.Launch("kill", pid);
                return Response.AsRedirect("/procs");
            };
        }
    }
}