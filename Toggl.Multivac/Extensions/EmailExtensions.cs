﻿namespace Toggl.Multivac.Extensions
{
    public static class EmailExtensions
    {
        public static Email ToEmail(this string self)
            => Email.FromString(self);
    }
}
