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

using Nancy.Security;
using Nancy;
using Antd.Reposotories;
using System.Linq;
using System.Dynamic;

namespace Antd.Modules {
    public class FileLayoutModule : NancyModule {
        private FileLayoutRepository repo = new FileLayoutRepository();

        public FileLayoutModule()
            : base("/filelayout") {
            this.RequiresAuthentication();

            Get["/"] = x => {
                dynamic vmod = new ExpandoObject();
                vmod.ALL = repo.GetAll();
                vmod.parents = new DirectoryLister("/etc", false).ParentList.Reverse();
                vmod.children2 = new DirectoryLister("/etc", false).FullList2;
                return View["page-file-layout", vmod];
            };

            Post["/"] = x => {
                string fileName = this.Request.Form.Name;
                string content = this.Request.Form.Content;
                string path = this.Request.Form.Path;
                string xt;
                if (this.Request.Form.Extns == "" || this.Request.Form.Extns == null) {
                    xt = "";
                }
                else {
                    xt = this.Request.Form.Extns;
                }
                repo.Create(fileName, content, path, xt);
                return Response.AsRedirect("/filelayout");
            };
        }
    }
}
