using System.Text;
using Entities.DTOs;
using Entities.Models;

namespace Server.Utility
{
    public static class UlTemplateGenerator
    {
        public static string GetHTMLString(RequestAbonentUpdateDto ra)
        {
            var address = string.Join(", ", ra.LocationAddressStreet, ra.LocationAddressBuilding, ra.LocationAddressBulk, ra.LocationAddressFlat).Replace(", ,", ",").Trim().TrimEnd(',');

            var sb = new StringBuilder();
            sb.AppendFormat(@$"<HTML>
                    <HEAD>
                    <META HTTP-EQUIV='Content-Type' CONTENT='text/html; CHARSET=utf-8'>
                    <TITLE></TITLE>

                    </HEAD>
                    <BODY >
                    <TABLE style='width:100%; height:0px; ' CELLSPACING=0>
                    <COL WIDTH=39>
                    <COL WIDTH=63>
                    <COL WIDTH=70>
                    <COL WIDTH=28>
                    <COL WIDTH=56>
                    <COL WIDTH=91>
                    <COL WIDTH=63>
                    <COL WIDTH=63>
                    <COL WIDTH=63>
                    <COL WIDTH=63>
                    <COL WIDTH=98>
                    <COL WIDTH=15>
                    <COL WIDTH=1>
                    <TR CLASS=R0>
                    <TD CLASS='R0C0'><SPAN></SPAN></TD>
                    <TD CLASS='R0C1' COLSPAN=11><SPAN STYLE='white-space:nowrap;max-width:0px;'>Удостоверяющий&nbsp;центр&nbsp;ООО&nbsp;«НПЦ&nbsp;«1С»<BR>119285,&nbsp;г.&nbsp;Москва,&nbsp;ул.&nbsp;Мосфильмовская,&nbsp;д.42,&nbsp;стр.1</SPAN></TD>
                    </TR>
                    <TR CLASS=R1>
                    <TD CLASS='R1C0'><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD CLASS='R1C0'><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:9px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C1' COLSPAN=11><SPAN STYLE='white-space:nowrap;max-width:0px;'>Заявление&nbsp;на&nbsp;выдачу&nbsp;квалифицированного&nbsp;сертификата&nbsp;в&nbsp;Удостоверяющем&nbsp;центре</SPAN></TD>

                    </TR>
                    <TR CLASS=R3>
                    <TD CLASS='R3C0'><SPAN></SPAN></TD> 
                    <TD CLASS='R3C1' COLSPAN=11>    {ra.LeaderPosition} организации {ra.ShortName} {ra.LeaderLastName} {ra.LeaderFirstName} {ra.LeaderPatronymic}, действуя на основании документа {ra.LeaderLegalDocument}, просит зарегистрировать уполномоченного представителя с личными данными: {ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic}, документ удостоверяющий личность {ra.PersonPassportSeries} {ra.PersonPassportNumber} от {ra.PersonPassportDate} выданный {ra.PersonPassportAddon}, код подразделения {ra.PersonPassportUnit}, создать квалифицированный сертификат ключа проверки электронной подписи Уполномоченного представителя Заявителя – юридического лица (Пользователя Удостоверяющего центра ООО «НПЦ «1С») в соответствии с данными, указанными в настоящем заявлении:</TD>

                    </TR>
                    <TR CLASS=R4>
                    <TD CLASS='R4C0'><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD CLASS='R4C0'><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD CLASS='R4C0'><DIV STYLE='position:relative; height:8px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Фамилия (SN)</TD>
                    <TD CLASS='R7C1' COLSPAN=4>{ra.PersonLastName}</TD>
                    <TD CLASS='R7C1'>СНИЛС</TD>
                    <TD CLASS='R5C8' COLSPAN=4>{ra.PersonSnils}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Имя Отчество (G)</TD>
                    <TD CLASS='R7C1' COLSPAN=4>{ra.PersonFirstName} {ra.PersonPatronymic}</TD>
                    <TD CLASS='R7C1'>E-Mail (E)</TD>
                    <TD CLASS='R5C8' COLSPAN=4>{ra.PersonEmail}</TD>

                    </TR>
                    <TR CLASS=R0>
                    <TD CLASS='R0C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Организация (O)</TD>
                    <TD CLASS='R7C3' COLSPAN=9>{ra.ShortName}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Должность (T)</TD>
                    <TD CLASS='R5C8' COLSPAN=4>{ra.PersonPost}</TD>
                    <TD CLASS='R7C1'>ИНН</TD>
                    <TD CLASS='R5C8' COLSPAN=4>{ra.PersonInn}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Страна (C)</TD>
                    <TD CLASS='R7C1' COLSPAN=4>RU</TD>
                    <TD CLASS='R7C1'>ОГРН</TD>
                    <TD CLASS='R5C8' COLSPAN=4>{ra.Ogrn}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R7C1' COLSPAN=2>Регион (S)</TD>
                    <TD CLASS='R7C1' COLSPAN=4>{ra.LocationAddressRegionId}</TD>
                    <TD CLASS='R7C1'>ИНН ЮЛ</TD>
                    <TD CLASS='R10C8' COLSPAN=4>{ra.Inn}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R5C8' COLSPAN=4>Населенный пункт (L)</TD>
                    <TD CLASS='R5C8' COLSPAN=7>{ra.LocationAddressCity}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R5C8' COLSPAN=4>Адрес местонахождения (STREET)</TD>
                    <TD CLASS='R5C8' COLSPAN=7>{address}</TD>

                    </TR>

                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>

                    </TR>
                    <TR CLASS=R25>
                    <TD ><SPAN></SPAN></TD>
                    <TD CLASS='R25C1' COLSPAN=11>    Ознакомлен(а) с Порядком УЦ и Пользовательским соглашением, в соответствии со статьёй 428 ГК Российской Федерации полностью и безусловно присоединяюсь к Порядку УЦ ООО «НПЦ «1С» и принимаю Пользовательское соглашение, условия которых опубликованы на сайте Удостоверяющего центра по адресу http://ca.1c.ru/reglament.pdf и на Портале информационно-технологического сопровождения по адресу https://portal.1c.ru/applications/31/licenseAgreement.
                        Я, {ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic}, документ удостоверяющий личность {ra.PersonPassportSeries} {ra.PersonPassportNumber} от {ra.PersonPassportDate} выданный {ra.PersonPassportAddon}, код подразделения {ra.PersonPassportUnit} в соответствии со статьей 9 Федерального закона от 27.07.2006 № 152-ФЗ «О персональных данных», даю согласие ООО «НПЦ «1С» (Фактический адрес: 127434, г. Москва, Дмитровское шоссе, д. 9) на обработку своих персональных данных: фамилия, имя, отчество (при наличии), ИНН, СНИЛС, место работы (организация), подразделение, должность, адрес регистрации, адрес электронной почты, пол, телефон, паспортные данные (серия и номер, код подразделения, место и дата рождения, дата выдачи паспорта, адрес регистрации), моего фотоизображения с паспортом, включая сбор, запись, систематизацию, накопление, хранение, уточнение (обновление, изменение), извлечение, использование, передачу (распространение, предоставление, доступ), обезличивание, блокирование, удаление, уничтожение) с использованием средств автоматизации и без использования таких средств.
                        Настоящее согласие на обработку персональных данных дается в целях исполнения договора на оказание услуг аккредитованного УЦ ООО «НПЦ «1С» по изготовлению квалифицированных сертификатов ключей проверки электронной подписи в соответствии с Федеральным законом от 06.04.2011 № 63-ФЗ «Об электронной подписи». Согласие на обработку персональных данных действует до истечения срока хранения информации УЦ ООО «НПЦ «1С» установленного п. 2 ст.15 Федерального закона от 06.04.2011 № 63-ФЗ «Об электронной подписи». Согласие на обработку персональных данных может быть отозвано мной в письменной форме.</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R18C1' COLSPAN=5>{ra.PersonPost}</TD>
                    <TD CLASS='R18C6' COLSPAN=6>____________ /{ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic} /</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R18C1' COLSPAN=5 ROWSPAN=2>{ra.LeaderPosition}</TD>
                    <TD CLASS='R18C6' COLSPAN=6 ROWSPAN=4>____________ / {ra.LeaderLastName} {ra.LeaderFirstName} {ra.LeaderPatronymic} /<BR>{DateTime.Now.ToString("dd.MM.yyyy")}</TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD CLASS='R2C0'><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD><DIV STYLE='position:relative; height:16px;width: 100%; overflow:hidden;'><SPAN></SPAN></DIV></TD>
                    <TD CLASS='R2C0'><SPAN STYLE='white-space:nowrap;max-width:0px;'>М.П.</SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>
                    <TD><SPAN></SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C1' COLSPAN=11><SPAN STYLE='white-space:nowrap;max-width:0px;'>Согласие&nbsp;на&nbsp;распространение&nbsp;персональных&nbsp;данных</SPAN></TD>

                    </TR>
                    <TR CLASS=R25>
                    <TD><SPAN></SPAN></TD>
                    <TD CLASS='R25C1' COLSPAN=11>    Я, {ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic}, телефон: {ra.Phone}, email: {ra.PersonEmail}, руководствуясь статьей 10.1 152-ФЗ «О персональных данных», заявляю согласие на распространение ООО «НПЦ «1С» ИНН 7729510210 (ОКВЭД:62.01, ОКПО:73827463, ОКОГУ: 4210014, ОКОПФ: 12300, ОКФС: 16, Фактический адрес: 127434, г. Москва, Дмитровское шоссе, д. 9) моих персональных данных с целью включения общих персональных данных (фамилия, имя, отчество (при наличии), СНИЛС, адрес электронной почты) в реестр квалифицированных сертификатов в соответствии с ч. 3 ст. 15 федерального закона от 06.04.2011 № 63-ФЗ «Об электронной подписи».
                        Условия и запреты на распространение персональных данных: _______________________________________ Согласие на распространение ООО «НПЦ «1С» персональных данных действует до истечения срока хранения информации удостоверяющим центром ООО «НПЦ «1С» установленного п. 6 ст.13 Федерального закона от 06.04.2011 № 63-ФЗ «Об электронной подписи». Информация из реестра квалифицированных сертификатов предоставляется путем направления запроса по email УЦ ca@1c.ru </TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD><SPAN></SPAN></TD>
                    <TD CLASS='R2C0' COLSPAN=11><SPAN STYLE='white-space:nowrap;max-width:0px;'>Субъект&nbsp;персональных&nbsp;данных</SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD><SPAN></SPAN></TD>
                    <TD CLASS='R18C1' COLSPAN=6 ROWSPAN=2>{ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic}</TD>
                    <TD CLASS='R28C7' COLSPAN=5><SPAN STYLE='white-space:nowrap;max-width:0px;'>____________&nbsp;/&nbsp;________________/</SPAN></TD>

                    </TR>
                    <TR CLASS=R2>
                    <TD><SPAN></SPAN></TD>
                    <TD CLASS='R29C11' COLSPAN=5><SPAN STYLE='white-space:nowrap;max-width:0px;'>{DateTime.Now.ToString("dd.MM.yyyy")}</SPAN></TD>

                    </TR>
                    </TABLE>
                    </BODY>
                    </HTML>");
            return sb.ToString();
        }
    }
}