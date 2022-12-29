using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Utility;

namespace Server.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    [Authorize]
    public class PdfGeneratorController : ControllerBase
    {
        private string htmlContent { get; set; }
        private readonly IConverter _converter;
        private readonly IQualifiedCertificateManager _sertifictaeManager;

        public PdfGeneratorController(IConverter converter, IQualifiedCertificateManager sertifictaeManager)
        {
            _sertifictaeManager = sertifictaeManager;
 
            _converter = converter;
            
        }

        [HttpPost]
        public IActionResult CreatePDF(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            

            if (requestAbonentUpdateDto.IsJuridical)
            {
                htmlContent = UlTemplateGenerator.GetHTMLString(requestAbonentUpdateDto); 
            }
            else
            {
                htmlContent = FlTemplateGenerator.GetHTMLString(requestAbonentUpdateDto);
            }
            var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "Заявление"
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = htmlContent,
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
        };
        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };
        var file = _converter.Convert(pdf);
        return File(file, "application/pdf", "Zayavlenie.pdf");
        }

        [Route("dover")]
        [HttpPost]
        public IActionResult CreateDovPDF(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Доверенность"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = DoverTemplateGenerator.GetHTMLString(requestAbonentUpdateDto),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "Doverennost.pdf");
        }


        [Route("cert/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> PrintCertificate(Guid id)
        {
            var certificateStructure = await _sertifictaeManager.GetCertificateData(id);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Доверенность"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = CertTemplateGenerator.GetHTMLString(certificateStructure),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "Doverennost.pdf");
        }


        
    }
}