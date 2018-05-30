namespace SquirrelFramework.Utility.Common.Http
{
    #region using directives

    using System;
    using System.Net;

    #endregion using directives

    public class CookieHelper
    {
        /// <summary>
        ///     Convert HTTP Header's Set-Cookie string to .NET CookieContainer
        /// </summary>
        public static CookieContainer ConvertToCookieContainer(string cookieString, string cookieDomain)
        {
            var result = new CookieContainer();
            if (string.IsNullOrEmpty(cookieString)) return result;
            var cookieStrings = cookieString.Split(';');
            foreach (var cookie in cookieStrings)
            {
                if (cookie.Contains(","))
                {
                    foreach (var cookieInner in cookie.Split(','))
                    {
                        var cookieInnerString = cookieInner.Split('=');
                        if (cookieInnerString.Length < 2) continue;
                        var cookieInnerStringKey = cookieInnerString[0].Trim();
                        var coolieInnerStringValue = cookieInnerString[1].Trim();
                        result.Add(
                            new Cookie(cookieInnerStringKey, coolieInnerStringValue)
                            {
                                Domain = cookieDomain,
                                Expires = DateTime.Now.AddDays(1)
                            });
                    }
                    continue;
                }

                var cookieStringNv = cookie.Split('=');
                if (cookieStringNv.Length < 2) continue;
                var cookieStringNvKey = cookieStringNv[0].Trim();
                var cookieStringNvValue = cookieStringNv[1].Trim();
                result.Add(new Cookie(cookieStringNvKey, cookieStringNvValue)
                {
                    Domain = cookieDomain
                });
            }
            return result;
        }
    }
}