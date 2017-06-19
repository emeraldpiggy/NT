using System;
using System.Collections.Generic;
using ServiceDomain;

namespace HubService.Interfaces
{
    /// <summary>
    /// This is the interface to access Ticker Class
    /// </summary>
    public interface ITicker:IDisposable
    {
        /// <summary>
        /// Gets all equities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Equity> GetAllEquities();

        /// <summary>
        /// Resets the market
        /// </summary>
        void Reset();
    }
}