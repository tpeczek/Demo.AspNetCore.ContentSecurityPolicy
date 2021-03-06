﻿using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Lib.AspNetCore.Security;
using Lib.AspNetCore.Security.Http.Reports;

namespace Demo.AspNetCore.Security.Services
{
    internal class LoggerSecurityHeadersReportingService : ISecurityHeadersReportingService
    {
        private readonly ILogger _logger;

        public LoggerSecurityHeadersReportingService(ILogger<ISecurityHeadersReportingService> logger)
        {
            _logger = logger;
        }

        public Task OnContentSecurityPolicyViolationAsync(ContentSecurityPolicyViolationReport report)
        {
            _logger.LogWarning("Content Security Policy Violation: Document: {DocumentUri} | Resource: {BlockedUri} | Directive: {ViolatedDirective} | Disposition: {Disposition}",
                report.DocumentUri,
                report.BlockedUri,
                report.ViolatedDirective,
                report.Disposition);

            return Task.FromResult(0);
        }

        public Task OnExpectCtViolationAsync(ExpectCtViolationReport report)
        {
            _logger.LogWarning("Expect-CT Violation: Failure Date: {FailureDate} UTC | Effective Expiration Date: {EffectiveExpirationDate} UTC | Host: {Host} | Port: {Port}",
                report.FailureDate.ToUniversalTime(),
                report.EffectiveExpirationDate.ToUniversalTime(),
                report.Hostname,
                report.Port);

            return Task.FromResult(0);
        }
    }
}
