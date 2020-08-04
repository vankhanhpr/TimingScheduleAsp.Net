using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportSoftWare.services;

namespace ReportSoftWare.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private IReport m_report;
        public ReportController(IReport report)
        {
            m_report = report;
        }
        [HttpGet("executeStore")]
        public dynamic get()
        {
            return m_report.execureQuery();
        }
    }
}