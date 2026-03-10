using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF operations
using Aspose.Pdf;        // Required for FormDataConverter

class FdfToXmlTransformer
{
    /// <summary>
    /// Converts the form data of a PDF file (exported as FDF) into an XML representation.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="xmlOutputPath">Path where the resulting XML file will be saved.</param>
    public static void TransformPdfToXml(string pdfPath, string xmlOutputPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Use the Form facade to work with AcroForm fields.
        using (Form form = new Form(pdfPath))
        {
            // Export the PDF form fields to an FDF stream.
            using (MemoryStream fdfStream = new MemoryStream())
            {
                form.ExportFdf(fdfStream);
                fdfStream.Position = 0; // Reset stream for reading.

                // Convert the FDF stream to XML using the static FormDataConverter method.
                using (FileStream xmlStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                {
                    FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
                }
            }
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "sample_form.pdf";
        const string outputXml = "form_data.xml";

        try
        {
            TransformPdfToXml(inputPdf, outputXml);
            Console.WriteLine($"Successfully converted '{inputPdf}' to XML at '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}