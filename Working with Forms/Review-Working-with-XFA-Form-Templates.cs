using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core API
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // List all XFA field names
            Console.WriteLine("XFA field names:");
            foreach (string name in doc.Form.XFA.FieldNames)
            {
                Console.WriteLine($" - {name}");
            }

            // Example: set a value for a specific XFA field (adjust the field name as needed)
            const string targetField = "CustomerName";
            if (doc.Form.XFA.FieldNames.Contains(targetField))
            {
                doc.Form.XFA[targetField] = "John Doe";
                Console.WriteLine($"Set field '{targetField}' to 'John Doe'.");
            }

            // Persist the changes to a new PDF file
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }

        // -----------------------------------------------------------------
        // Extract the raw XFA data packet using the Facade API (Form class)
        // -----------------------------------------------------------------
        const string xfaExtractPath = "xfa_data.xml";
        using (FileStream extractStream = new FileStream(xfaExtractPath, FileMode.Create, FileAccess.Write))
        {
            Form formFacade = new Form(inputPdf);
            formFacade.ExtractXfaData(extractStream);
            formFacade.Close(); // Form does not implement IDisposable, so close explicitly
            Console.WriteLine($"Extracted XFA data to '{xfaExtractPath}'.");
        }

        // ---------------------------------------------------------------
        // Replace the XFA data packet with a modified XML file (if any)
        // ---------------------------------------------------------------
        const string modifiedXfaPath = "xfa_data_modified.xml";
        if (File.Exists(modifiedXfaPath))
        {
            Form formFacade = new Form(inputPdf);
            using (FileStream modifiedXfaStream = new FileStream(modifiedXfaPath, FileMode.Open, FileAccess.Read))
            {
                formFacade.SetXfaData(modifiedXfaStream);
            }
            // Save the PDF with the updated XFA data
            formFacade.Save(outputPdf);
            formFacade.Close();
            Console.WriteLine($"PDF with updated XFA saved to '{outputPdf}'.");
        }
        else
        {
            Console.WriteLine($"Modified XFA file not found: {modifiedXfaPath}");
        }
    }
}