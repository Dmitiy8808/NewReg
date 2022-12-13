using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Utility;

namespace Server.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {
        private readonly IConverter _converter;
        private readonly IMapper _mapper;
        public PdfGeneratorController(IConverter converter, IMapper mapper)
        {
            _mapper = mapper;
            _converter = converter;
            
        }

        [HttpGet]
        public IActionResult CreatePDF(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var globalSettings = new GlobalSettings
    {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "PDF Report"
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = UlTemplateGenerator.GetHTMLString(requestAbonentUpdateDto),
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
        };
        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };
        var file = _converter.Convert(pdf);
        return File(file, "application/pdf", "EmployeeReport.pdf");
        }
        
    }
}