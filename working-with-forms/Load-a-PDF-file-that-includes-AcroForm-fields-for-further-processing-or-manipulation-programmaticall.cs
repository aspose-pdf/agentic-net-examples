using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input_with_form.pdf";   // PDF that contains AcroForm fields
        const string outputPdfPath = "processed_output.pdf"; // Path for the saved PDF
        const string xfdfPath      = "data.xfdf";            // Optional XFDF file with field values

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind a Form facade to the loaded document
            using (Form formFacade = new Form(pdfDoc))
            {
                // ------------------------------------------------------------
                // Example 1: List all form field names present in the PDF
                // ------------------------------------------------------------
                Console.WriteLine("Form fields found in the document:");
                foreach (string fieldName in formFacade.FieldNames)
                {
                    Console.WriteLine($" - {fieldName}");
                }

                // ------------------------------------------------------------
                // Example 2: Fill a specific field programmatically
                // ------------------------------------------------------------
                // Replace "CustomerName" with the actual field name in your PDF
                const string targetField = "CustomerName";
                const string fieldValue  = "John Doe";

                // The FillField method works with full field names only
                formFacade.FillField(targetField, fieldValue);
                Console.WriteLine($"Field '{targetField}' set to '{fieldValue}'.");

                // ------------------------------------------------------------
                // Example 3: Import field values from an XFDF file (optional)
                // ------------------------------------------------------------
                if (File.Exists(xfdfPath))
                {
                    using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                    {
                        // XfdfReader reads field values from the XFDF stream and applies them to the document
                        XfdfReader.ReadFields(xfdfStream, pdfDoc);
                        Console.WriteLine("Field values imported from XFDF.");
                    }
                }

                // ------------------------------------------------------------
                // Save the modified PDF document
                // ------------------------------------------------------------
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}