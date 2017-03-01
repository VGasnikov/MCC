<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="yes" />

  <xsl:param name="caption"/>
  <xsl:param name="piefillAlpha"/>
  <xsl:param name="pieBorderThickness"/>
  <xsl:param name="hoverFillColor"/>
  <xsl:param name="pieBorderColor"/>
  <xsl:param name="useHoverColor"/>
  <xsl:param name="basefontColor"/>
  <xsl:param name="baseFont"/>
  <xsl:param name="baseFontSize"/>
  <xsl:param name="showToolTipShadow"/>
  <xsl:param name="TrancateChartLabels"/>
  <xsl:param name="NumberofCharacters"/>
  <xsl:param name="chartShowBorder"/>
  <xsl:param name="bgcolor"/>
  <xsl:param name="RootAudience"/>
  
  
  <xsl:template match='/'>
    <xsl:element name='chart'>
      <xsl:attribute name='caption'>
        <xsl:value-of select="$caption"/>
      </xsl:attribute>
      <xsl:attribute name='piefillAlpha'>
        <xsl:value-of select="$piefillAlpha"/>
      </xsl:attribute>
      <xsl:attribute name='pieBorderThickness'>
        <xsl:value-of select="$pieBorderThickness"/>
      </xsl:attribute>
      <xsl:attribute name='hoverFillColor'>
        <xsl:value-of select="$hoverFillColor"/>
      </xsl:attribute>
      <xsl:attribute name='pieBorderColor'>
        <xsl:value-of select="$pieBorderColor"/>
      </xsl:attribute>
      <xsl:attribute name='useHoverColor'>
        <xsl:value-of select="$useHoverColor"/>
      </xsl:attribute>

      <xsl:attribute name='basefontColor'>
        <xsl:value-of select="$basefontColor"/>
      </xsl:attribute>
      <xsl:attribute name='baseFont'>
        <xsl:value-of select="$baseFont"/>
      </xsl:attribute>
      <xsl:attribute name='baseFontSize'>
        <xsl:value-of select="$baseFontSize"/>
      </xsl:attribute>
      <xsl:attribute name='showToolTipShadow'>
        <xsl:value-of select="$showToolTipShadow"/>
      </xsl:attribute>
      <xsl:attribute name='showBorder'>
        <xsl:value-of select="$chartShowBorder"/>
      </xsl:attribute>
      <xsl:attribute name='bgcolor'>
        <xsl:value-of select="$bgcolor"/>
      </xsl:attribute>
      <xsl:apply-templates select='audiences'/>
    </xsl:element>
  </xsl:template>

  <xsl:template match='audiences'>
    <xsl:for-each select='audience[@id=$RootAudience]'>
      <xsl:element name='category'>
        <xsl:attribute name='label'><xsl:value-of select='@name' /></xsl:attribute>
        <xsl:attribute name='hoverText'><xsl:value-of select='@description' /></xsl:attribute>
        <xsl:attribute name='link'><xsl:value-of select='@link' /></xsl:attribute>
        <xsl:apply-templates select='audience'/>
      </xsl:element>
    </xsl:for-each>

  </xsl:template>


  <xsl:template match='audience/*'>
    <xsl:element name='category'>
      <xsl:attribute name='label'>
        <xsl:choose><xsl:when test="$TrancateChartLabels ='01'"><xsl:value-of select="substring(@name,0,$NumberofCharacters)"/>..</xsl:when>
          <xsl:otherwise><xsl:value-of select='@name' /></xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name='hoverText'><xsl:value-of select='@description' /></xsl:attribute>
      <xsl:attribute name='link'><xsl:value-of select='@link' /></xsl:attribute>
      <xsl:apply-templates select='*'/>
    </xsl:element>
  </xsl:template>

</xsl:stylesheet>