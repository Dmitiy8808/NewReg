using System.Text;
using Entities.DTOs;
using Entities.Models;

namespace Server.Utility
{
    public static class CertTemplateGenerator
    {
        public static string GetHTMLString(CertificateStructureDto cs)
        {
            

            var sb = new StringBuilder();
            sb.AppendFormat(@$"<HTML>
                    <HEAD>
                    <META HTTP-EQUIV='Content-Type' CONTENT='text/html; CHARSET=utf-8'>
                    <TITLE></TITLE>

                    </HEAD>
                        <body>

      <div class='block'>
        <div class='img' height='49' width='110'>
          <img src='logo.png'/>
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

      <div style='position:absolute; top:150px; left:60px; width:960px;'>

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
            <xsl:value-of select='//cert/subject/@OGRN'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Идентификационный номер налогоплательщика: {cs.PersonInn} </b>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Индивидуальный номер налогоплательщика: </b>
            <xsl:choose>
              <xsl:when test='//cert/subject/@InnLe != '''>
                <xsl:value-of select='//cert/subject/@INN'/>
              </xsl:when>
            </xsl:choose>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Страховой номер индивидуального лицевого счета: </b>
            <xsl:value-of select='//cert/subject/@SNILS'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Электронная почта: </b>
            <xsl:value-of select='//cert/subject/@EMAIL'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Место нахождения юридического лица: </b>
            <xsl:value-of select='//cert/subject/@location'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Уполномоченный представитель юридического лица: </b>
            <xsl:value-of select='//cert/subject/@T'/>&#160;<xsl:value-of select='//cert/subject/@SN'/>&#160;<xsl:value-of select='//cert/subject/@G'/>
          </div>
          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Тип идентификации при выдаче сертификата:  </b>
            <xsl:value-of select='//cert/IdentificationKind/@value'/>
          </div>



          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Сведения об издателе квалифицированного сертификата</b>
          </pre>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@CommonName'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Место нахождения удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@location'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Номер квалифицированного сертификата удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@SN'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование средства электронной подписи: </b>
            <xsl:value-of select='//cert/Issuer/@ElectronicSignatureName'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Реквизиты заключения о подтверждении соответствия средства электронной подписи: </b>
            <xsl:value-of select='//cert/Issuer/@ElectronicSignatureReqv'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Наименование средства удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@CertificationAuthorityName'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Реквизиты заключения о подтверждении соответствия средства удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@CertificationAuthorityReqv'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Класс средств удостоверяющего центра: </b>
            <xsl:value-of select='//cert/Issuer/@CertificatePolicies'/>
          </div>

          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Сведения о ключе проверки электронной подписи</b>
          </pre>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемый алгоритм: </b>
            <xsl:value-of select='//cert/openKey/@parameter'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемое средство электронной подписи: </b>
            <xsl:value-of select='//cert/subject/@ElectronicSignatureTool'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Класс средства электронной подписи: </b>
            <xsl:value-of select='//cert/openKey/@CertificatePolicies'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Область использования ключа: </b>
            <xsl:value-of select='//cert/openKey/@KeyUsage'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Значение ключа: </b>
            <xsl:value-of select='//cert/openKey/@algoritm'/>
          </div>


          <pre style='font-size:14pt'>
            <b>&#13;&#x9;&#x9;Электронная подпись под квалифицированным сертификатом</b>
          </pre>
          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Используемый алгоритм: </b>
            <xsl:value-of select='//cert/openKey/@algoritm'/>
          </div>

          <div style='margin-left: 65px;font-size:12pt'>
            <b style='font-size:12pt'>Значение электронной подписи: </b>
            <xsl:for-each select='cert/ElectronicSignature/signatureValue/signatureString'>
              <div style='margin-left: 65px;font-size:12pt'>
                <xsl:value-of select='@value'/>
              </div>

            </xsl:for-each>
          </div>

          <br></br>
          <div style='margin-left: 65px;font-size:12pt'>
            Подпись владельца сертификата _____________________________/ <xsl:value-of select='//cert/subject/@SN'/>&#160;<xsl:value-of select='//cert/subject/@G'/> /
            <pre>  &#x9;&#x9; &#x9; &#x9; &#x9; М.П.</pre>
          </div>
        </FONT>
      </div>

    </body>
                    </HTML>");
            return sb.ToString();
        }
    }
}