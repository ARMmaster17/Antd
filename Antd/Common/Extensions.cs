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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Antd.Common {

    public static class Extensions {

        public static string GetFirstString(this String str) {
            var arr = str.Split(' ');
            return arr.Length > 0 ? arr[0] : String.Empty;
        }

        public static string GetFirstString(this String str, char div) {
            var arr = str.Split(div);
            return arr.Length > 0 ? arr[0] : String.Empty;
        }

        public static string GetAllStringsButFirst(this String str) {
            var arr = str.Split(' ');
            //arr.Skip(1).ToArray();
            if (arr.Length > 1) {
                return string.Join(" ", arr.Skip(1).ToArray());
            }
            else {
                return String.Empty;
            }
        }

        public static string GetAllStringsButFirst(this String str, char div) {
            var arr = str.Split(div).Skip(1).ToArray();
            //arr.Skip(1).ToArray();
            if (arr.Length > 1) {
                return string.Join(" ", arr);
            }
            else {
                return String.Empty;
            }
        }

        public static string UppercaseAllFirstLetters(this String str) {
            var arr = str.Split(' ');
            var newList = new List<string>() { };
            newList.AddRange(arr.Select(a => a.UppercaseFirstLetter()));
            return string.Join(" ", newList.ToArray());
        }

        public static string UppercaseAllFirstLetters(this String str, char div) {
            var arr = str.Split(' ');
            var newList = new List<string>() { };
            newList.AddRange(arr.Select(a => a.UppercaseFirstLetter()));
            return string.Join(div.ToString(), newList.ToArray());
        }

        public static string UppercaseFirstLetter(this String str) {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string RemoveWhiteSpace(this String str) {
            return str.Replace(" ", "");
        }
    }
}