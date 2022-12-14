using System.Text;
using Entities.DTOs;
using Entities.Models;

namespace Server.Utility
{
    public static class DoverTemplateGenerator
    {
        public static string GetHTMLString(RequestAbonentUpdateDto ra)
        {
            

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
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD  COLSPAN=2></TD>
                    <TD COLSPAN=4></TD>
                    <TD ></TD>
                    <TD  COLSPAN=4></TD>

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
                    <TD CLASS='R2C1' COLSPAN=11><SPAN STYLE='white-space:nowrap;max-width:0px;'>Доверенность</SPAN></TD>

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
                    <TD CLASS='R5C1' COLSPAN=2>город {ra.LocationAddressCity}</TD>
                    <TD COLSPAN=4></TD>
                    <TD ></TD>
                    <TD CLASS='R5C9' COLSPAN=4>{DateTime.Now.ToString("dd.MM.yyyy")}</TD>
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

                    <TR CLASS=R3>
                    <TD CLASS='R3C0'><SPAN></SPAN></TD> 
                    <TD CLASS='R3C1' COLSPAN=11> {ra.LeaderPosition} организации {ra.ShortName} {ra.LeaderLastName} {ra.LeaderFirstName} {ra.LeaderPatronymic}, действуя на основании документа {ra.LeaderLegalDocument}, уполномачивает представителя с личными данными: {ra.PersonLastName} {ra.PersonFirstName} {ra.PersonPatronymic}, документ удостоверяющий личность {ra.PersonPassportSeries} {ra.PersonPassportNumber} от {ra.PersonPassportDate} выданный {ra.PersonPassportAddon}, код подразделения {ra.PersonPassportUnit} <ol>
                    <li>Выступать в роли Пользователя Удостоверяющего центра и осуществлять действия в рамках Регламента УЦ, установленные для Пользователя УЦ.</li>
                    <li>Предоставить в УЦ необходимые документы для регистрации, определенные Регламентом УЦ.</li>
                    <li>Получить сертификат ЭП уполномоченного лица УЦ, сформированные ключи подписи и сертификат ЭП Пользователя Удостоверяющего центра и иные документы, определенные Регламентом УЦ.</li>
                    <li>Представитель наделяется правом расписываться на копии сертификата ЭП на бумажном носителе и в соответствующих документах для исполнения поручений, определенных настоящей доверенностью.</li>
                    </ol></TD>

                    </TR>
                    <TR CLASS=R3>
                    <TD CLASS='R3C0'><SPAN></SPAN></TD> 
                    <TD CLASS='R3C1' COLSPAN=11> Настоящая доверенность действительна по «___» ___________ 20___ г.</TD>

                    </TR>

                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R18C1' COLSPAN=5>Подпись уполномоченного представителя подтверждаю</TD>
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
                    <TD CLASS='R18C1' COLSPAN=5>{ra.LeaderPosition}</TD>
                    <TD CLASS='R18C6' COLSPAN=6>____________ /{ra.LeaderLastName} {ra.LeaderFirstName} {ra.LeaderPatronymic}/</TD>

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
                    <TD  COLSPAN=4>«___» ___________ 20___ г.</TD>
                    <TD COLSPAN=4></TD>
                    <TD ></TD>
                    <TD  COLSPAN=2></TD>

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

                    </TR>
                    </TR>
                    <TR CLASS=R2>
                    <TD CLASS='R2C0'><SPAN></SPAN></TD>
                    <TD CLASS='R5C2' COLSPAN=4>М.П.</TD>
                    <TD COLSPAN=4></TD>
                    <TD ></TD>
                    <TD  COLSPAN=2></TD>

                    </TR>
                   
                    </TABLE>
                    </BODY>
                    </HTML>");
            return sb.ToString();
        }
    }
}