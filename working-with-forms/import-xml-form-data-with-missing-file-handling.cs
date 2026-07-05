using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the XML file that contains form data.
        const string pdfPath = "input.pdf";
        const string xmlPath = "formData.xml";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Check whether the XML file is present before attempting to import it.
            if (File.Exists(xmlPath))
            {
                try
                {
                    // Bind the XML data to the PDF. This imports XFA form data if present.
                    // No load options are required for BindXml; the method reads the file directly.
                    pdfDoc.BindXml(xmlPath);
                    Console.WriteLine("XML data successfully bound to the PDF.");
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors during the binding process.
                    Console.Error.WriteLine($"Warning: Failed to bind XML file – {ex.Message}");
                }
            }
            else
            {
                // XML file is missing – handle gracefully without throwing.
                Console.WriteLine($"Info: XML file '{xmlPath}' not found. Skipping XML import.");
            }

            // Optionally, add a visual indication that the import was attempted.
            // Here we add a simple text annotation on the first page.
            if (pdfDoc.Pages.Count > 0)
            {
                Page firstPage = pdfDoc.Pages[1];
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation note = new TextAnnotation(firstPage, rect)
                {
                    Title = "Import Status",
                    Contents = File.Exists(xmlPath) ? "XML data imported." : "No XML data.",
                    Open = true,
                    Color = Aspose.Pdf.Color.Yellow,
                    Icon = TextIcon.Note
                };
                firstPage.Annotations.Add(note);
            }

            // Save the resulting PDF.
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}