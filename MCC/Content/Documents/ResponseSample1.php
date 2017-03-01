<html>
<head>
<title>Response Result</title>
<style type="text/css">
td.span1
{
color: black;
font-size: 14pt; 
font-family: arial
}
td.span2
{
color: white;
font-size: 12pt; 
font-family: arial
}
td.span3
{
color: black;
font-size: 11pt; 
font-family: arial
}
</style>
<?php
$TransID = $_REQUEST['TransID'];
$RefNo = $_REQUEST['RefNo'];
$Notes = $_REQUEST['Notes'];
$Auth = $_REQUEST['Auth'];
$AVSCode = $_REQUEST['AVSCode'];
$CVV2ResponseMsg = $_REQUEST['CVV2ResponseMsg'];
?>
</head>
<body>
<CENTER> 
<table border="0" cellpadding="4" cellspacing="4" style="border-collapse: collapse" bordercolor="#111111" width="672">
<tr>
        <td align="center">
                                <img border="0" src="images/BanxHCMH.gif" width="318" height="67">
        </td>
</tr>
<tr>
        <td align="center" class="span1">
                <b>Bill Payment Online Order Confirmation</b><br><br>
        </td>
</tr>
</table>
<? 

if (($TransID != null) || ($Notes != null) and ($TransID != 0) )
{
  if (($Auth=='Declined') || (empty($Auth))) 
  {
    echo "<table width=\"50%\" border=\"0\" cellpadding=\"1\" cellspacing=\"2\" bgcolor=\"#006565\">
 <tr>
    <td><table width=\"100%\" border=\"0\" cellpadding=\"1\" cellspacing=\"5\" bgcolor=\"#FFFFFF\">
        <tr>
          <td bgcolor=\"#006565\" class=\"span3\"><div align=\"center\"><strong>Sorry, Your Transaction Has Been Declined.</strong></div></td>
        </tr>
        <tr> 
          <td><div align=\"center\"> </div>
		<TABLE width=\"75%\" align=\"center\" cellspacing=\"0\" bgcolor=\"#FFFFFF\">
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Transaction ID:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $TransID</B></TD>
			  </TR>		 
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Authorization Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Auth</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Reference Number:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $RefNo</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>AVS Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $AVSCode</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Notes:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Notes</B></TD>
			  </TR>	 
			  <TR>
          <td colspan = \"2\" align=\"center\"><br></td>
			  </TR>
		  <tr> 
          <td colspan = \"2\" align=\"center\" class=\"span3\"><strong><a href=\"index.html\">Return to Home Page</a></strong></td>
          </tr>
		</TABLE>
			</td>
        </tr>
      </table></td>
  </tr>
</table> ";

  } 
  else
  {		
    echo "<table width=\"50%\" border=\"0\" cellpadding=\"1\" cellspacing=\"2\" bgcolor=\"#006565\">
  <tr>
    <td><table width=\"100%\" border=\"0\" cellpadding=\"1\" cellspacing=\"5\" bgcolor=\"#FFFFFF\">
        <tr>
          <td bgcolor=\"#006565\" class=\"span2\"><div align=\"center\">Your Transaction Has Been Approved.</b></div></td>
        </tr>
        <tr> 
          <td><div align=\"center\">
		<TABLE width=\"75%\" align=\"center\" cellspacing=\"0\" bgcolor=\"#FFFFFF\">
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Transaction ID:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $TransID</B></TD>
			  </TR>		 
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Authorization Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Auth</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Reference Number:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $RefNo</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>AVS Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $AVSCode</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Notes:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Notes</B></TD>
			  </TR>	 
			  <TR>
          <td colspan = \"2\" align=\"center\"><br></td>
			  </TR>
		  <tr> 
          <td colspan = \"2\" align=\"center\" class=\"span3\"><strong><a href=\"index.html\">Return to Home Page</a></strong></td>
          </tr>
		</TABLE>
</td>
        </tr>
      </table></td>
  </tr>
</table>";
  }
}
else 
{
  echo "
  <table width=\"50%\" border=\"0\" cellpadding=\"1\" cellspacing=\"2\" bgcolor=\"#006565\">
  <tr>
    <td><table width=\"100%\" border=\"0\" cellpadding=\"1\" cellspacing=\"5\" bgcolor=\"#FFFFFF\">
        <tr>
          <td bgcolor=\"#006565\" class=\"span2\"><div align=\"center\">Your Transaction Was Not Processed.</b></div></td>
        </tr>
        <tr> 
          <td> 
		<TABLE width=\"75%\" align=\"center\" cellspacing=\"0\" bgcolor=\"#FFFFFF\">
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Transaction ID:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $TransID</B></TD>
			  </TR>		 
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Authorization Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Auth</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Reference Number:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $RefNo</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>AVS Code:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $AVSCode</B></TD>
			  </TR>
			  <TR>
				 <TD align=\"right\" width=\"50%\" class=\"span3\"><B>Notes:</B></TD>
				 <TD width=\"50%\" class=\"span3\"><B>&nbsp; &nbsp; $Notes</B></TD>
			  </TR>	 
			  <TR>
          <td colspan = \"2\" align=\"center\"><br></td>
			  </TR>
		  <tr> 
          <td colspan = \"2\" align=\"center\" class=\"span3\"><strong><a href=\"index.html\">Return to Home Page</a></strong></td>
          </tr>
		</TABLE>
		  </td>
        </tr>
      </table></td>
  </tr>
</table>";	
}
?>	 
</body>
</html>
