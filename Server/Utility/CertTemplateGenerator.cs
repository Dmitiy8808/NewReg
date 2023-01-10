using System.Text;
using Entities.DTOs;
using Entities.Models;

namespace Server.Utility
{
    public static class CertTemplateGenerator
    {
        public static string GetHTMLString(CertificateStructureDto cs)
        {
          var logo =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png");
            

            var sb = new StringBuilder();
            sb.AppendFormat(@$"<HTML>
                    <HEAD>
                    <META HTTP-EQUIV='Content-Type' CONTENT='text/html; CHARSET=utf-8'>
                    <TITLE></TITLE>

                    </HEAD>
                        <body>

      <div class='block' style='margin-left: 120px;'>
        <div class='img' height='49' width='110'>
          <img src='{logo}'/>
        </div>
        <div class='text'>
          <b>
            <p>Научно-производственный центр «1С»</p>
          </b>
        </div>
        <div class='text2'>
          <b>
            <p>г.Москва, ул.Селезневская, д.21 тел.(495) 681-37-63 , факс (495) 681-37-63</p>
          </b>
        </div>
      </div>

      <div style='position:absolute; top:150px; left:60px; width:1200px;'>

        <FONT SIZE='6.8'>
          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Номер квалифицированного сертификата: </b>
            {cs.SerialNumber}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Действие квалифицированного сертификата: </b>
            c {cs.NotBefore} по {cs.NotAfter}
          </div>


          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Сведения о владельце квалифицированного сертификата</b>
          </pre>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование юридического лица: </b>
            {cs.Organization}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Основной государственный регистрационный номер: </b>
            {cs.Orgn}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Идентификационный номер налогоплательщика:  </b>
            {cs.Inn}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Индивидуальный номер налогоплательщика: </b>
            {cs.PersonInn}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Страховой номер индивидуального лицевого счета: </b>
            {cs.Snils}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Электронная почта: </b>
            {cs.Email}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Место нахождения юридического лица: </b>
            {cs.AuthorityAddressCity} {cs.AuthorityAddressStreet}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Уполномоченный представитель юридического лица: </b>
            {cs.FirstName} {cs.GivenName}
          </div>
          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Тип идентификации при выдаче сертификата:  </b>
            {cs.IdentificationKind}
          </div>



          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Сведения об издателе квалифицированного сертификата</b>
          </pre>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование удостоверяющего центра: </b>
            {cs.AuthorityName}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Место нахождения удостоверяющего центра: </b>
            {cs.AuthorityAddressCity} {cs.AuthorityAddressStreet}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Номер квалифицированного сертификата удостоверяющего центра: </b>
            {cs.AuthoritySerialNumber}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование средства электронной подписи: </b>
            {cs.AuthoritySignTool}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Реквизиты заключения о подтверждении соответствия средства электронной подписи: </b>
            {cs.AuthoritySignToolCertificate}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование средства удостоверяющего центра: </b>
            {cs.AuthorityCaTool}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Реквизиты заключения о подтверждении соответствия средства удостоверяющего центра: </b>
            {cs.AuthorityCaCertificate}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Класс средств удостоверяющего центра: </b>
            {cs.AuthorityType}
          </div>

          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Сведения о ключе проверки электронной подписи</b>
          </pre>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемый алгоритм: </b>
            {cs.CertAlgorithm} 
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемое средство электронной подписи: </b>
            {cs.CertSignTool}  
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Класс средства электронной подписи: </b>
            {cs.CertSignType}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Область использования ключа: </b>
            {cs.CertKeyUsage}
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Значение ключа: </b>
            <br>
            {cs.PublicKey}
          </div>


          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Электронная подпись под квалифицированным сертификатом</b>
          </pre>
          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемый алгоритм: </b>
            {cs.CertAlgorithm} 
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Значение электронной подписи: </b>
            <br>
            {cs.Signature}
          </div>

          <br></br>
          <div style='margin-left: 65px;font-size:12pt'>
            Подпись владельца сертификата _____________________________/ {cs.FirstName} {cs.GivenName}
            
          </div>
        </FONT>
      </div>

    </body>
                    </HTML>");
            return sb.ToString();
        }
    }
}