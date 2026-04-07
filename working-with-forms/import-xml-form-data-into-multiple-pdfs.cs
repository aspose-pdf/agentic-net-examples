using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input XML containing form field values
        const string xmlDataPath = "formData.xml";

        // List of PDF files to which the same XML data will be applied
        string[] pdfInputs = { "document1.pdf", "document2.pdf", "document3.pdf" };

        // Verify XML file exists
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Process each PDF
        foreach (string inputPdf in pdfInputs)
        {
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPdf}");
                continue;
            }

            // Determine output file name (e.g., document1_filled.pdf)
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_filled.pdf");

            // Use Form facade to bind the PDF, import XML data, and save the result
            using (Form form = new Form())
            {
                // Bind the source PDF
                form.BindPdf(inputPdf);

                // Open the XML stream and import the data into the form fields
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the updated PDF
                form.Save(outputPdf);
            }

            Console.WriteLine($"Processed '{inputPdf}' → '{outputPdf}'");
        }
    }
}