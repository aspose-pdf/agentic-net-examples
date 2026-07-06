using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment, FontRepository, TextState

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlPath = "pagination.xml";   // XML containing pagination logic
        const string xslPath = "pagination.xsl";   // XSLT that defines pagination artifacts
        const string outputPdfPath = "output_paginated.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Apply XSLT logic from the XML file.
                // BindXml with an XSLT file merges the transformed content into the PDF.
                pdfDoc.BindXml(xmlPath, xslPath);

                // -----------------------------------------------------------------
                // Pagination – Aspose.Pdf versions prior to the introduction of the
                // PageNumberArtifact class (or when the class is unavailable) require
                // manual insertion of page numbers.  The following loop adds a simple
                // page‑number footer to every page using a TextFragment.  This approach
                // stays within the allowed namespaces (no Facades or Plugins) and works
                // for all supported Aspose.Pdf versions.
                // -----------------------------------------------------------------
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    // Create a TextFragment containing the current page number.
                    TextFragment pageNumber = new TextFragment(i.ToString())
                    {
                        // Position the fragment near the bottom‑center of the page.
                        // Adjust the coordinates as needed for your layout.
                        Position = new Position(0, 20), // X = 0 (center), Y = 20 points from bottom
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        // Optional styling – you can change font, size, color, etc.
                        TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
                    };
                    page.Paragraphs.Add(pageNumber);
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Paginated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
