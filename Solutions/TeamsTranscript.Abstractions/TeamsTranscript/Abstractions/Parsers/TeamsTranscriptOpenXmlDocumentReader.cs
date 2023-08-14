// <copyright file="TeamsTranscriptOpenXmlDocumentReader.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace TeamsTranscript.Abstractions.Parsers;

public class TeamsTranscriptOpenXmlDocumentReader : ITeamsTranscriptDocumentReader
{
    public string Read(Stream contents)
    {
        using WordprocessingDocument doc = WordprocessingDocument.Open(contents, isEditable: false);
        StringBuilder sb = new();
        IEnumerable<OpenXmlElement>? elements = doc.MainDocumentPart?.Document?.Body?.Elements();

        if (elements is not null)
        {
            foreach (OpenXmlElement el in elements)
            {
                foreach (OpenXmlElement tr in el)
                {
                    foreach (OpenXmlElement run in tr)
                    {
                        if (!string.IsNullOrEmpty(run.InnerText))
                        {
                            sb.AppendLine(run.InnerText);
                        }
                    }
                }
            }
        }

        return sb.ToString();
    }
}