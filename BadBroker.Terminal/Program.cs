using System;
using System.IO;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace BadBroker.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BadBrokerContext badBrokerContext = new BadBrokerContext())
            {
                QuotesData quotesData = new QuotesData();
                quotesData.StartDate = "qqq";
                quotesData.EndDate = "www";

                badBrokerContext.QuotesData.Add(quotesData);
                badBrokerContext.SaveChanges();
            }
        }
    }
}
