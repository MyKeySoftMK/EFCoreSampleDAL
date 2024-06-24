using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EFDesigner2022Tests
{
    public partial class EFModelDatabase
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        public static void  ConfigureOptions(DbContextOptionsBuilder<EFModelDatabase> optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message))
                          .UseSqlServer(ConnectionString);
        }
    }
}
