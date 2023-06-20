﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreClaims.Infrastructure;
using CoreClaims.Infrastructure.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CoreClaims.FunctionApp.HttpTriggers
{
    public class GetMember
    {
        [Function("GetMember")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "member/{memberId}")] HttpRequest req,
            [CosmosDBInput(
                databaseName: Constants.Connections.CosmosDbName, 
                containerName: "Member", 
                Connection = Constants.Connections.CosmosDb,
                PartitionKey = "{memberId}", Id = "{memberId}")] Member member,
            ILogger log)
        {
            using (log.BeginScope("HttpTrigger: GetMember"))
            {
                if (member == null) return new NotFoundResult();

                return new OkObjectResult(member);
            }
        }
    }
}
