using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the FDF data file, the XML export, and the resulting PDF
        const string pdfInputPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string xmlExportPath = "formData.xml";
        const string pdfOutputPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF input file not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(pdfInputPath);

            // -------------------------------------------------
            // Export AcroForm fields to XML (to a file and a stream)
            // -------------------------------------------------
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Save the document's form data as XML into the stream
                pdfDoc.Save(xmlStream, new XmlSaveOptions());

                // Persist the XML to a file for inspection
                File.WriteAllBytes(xmlExportPath, xmlStream.ToArray());
            }

            // -------------------------------------------------
            // Import data from an FDF file into the PDF's form
            // -------------------------------------------------
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Reads annotations (including form field values) from the FDF stream
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the updated PDF document
            pdfDoc.Save(pdfOutputPath);

            Console.WriteLine("PDF processing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}