using System;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{
    /// <summary>
    /// The Implementation of TheSystem interface
    /// </summary>
    public class TheSystemImpl : TheSystemDisp_
    {
        /// <summary>
        /// The Logger
        /// </summary>
        private readonly ILogger<TheSystemImpl> _logger;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="serviceScopeFactory">The Scope</param>
        public TheSystemImpl(ILogger<TheSystemImpl> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _logger.LogDebug("Building TheSystemImpl...");
        }

        /// <summary>
        /// Return the difference in time
        /// </summary>
        /// <param name="clienTime"></param>
        /// <param name="current"></param>
        /// <returns>The Delay</returns>
        public override long getDelay(long clienTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clienTime;
        }
    }
}