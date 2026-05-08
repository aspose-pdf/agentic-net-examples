using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_modified.pdf";
        const string xfdfPath      = "annotations.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export existing annotations to XFDF file
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);

                // Load the XFDF XML, modify annotation colors, and save it back
                XDocument xfdfXml = XDocument.Load(xfdfPath);

                // XFDF stores annotation color in the "c" attribute of <annot> elements.
                // Change all colors to red (RGB 1 0 0) – XFDF uses a hex string without '#'.
                foreach (var annot in xfdfXml.Descendants("annot"))
                {
                    // Set the "c" attribute to red (hex FF0000)
                    annot.SetAttributeValue("c", "FF0000");
                }

                // Save the modified XFDF back to the same file
                xfdfXml.Save(xfdfPath);

                // Import the modified annotations back into the PDF
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations exported, colors changed, and PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}