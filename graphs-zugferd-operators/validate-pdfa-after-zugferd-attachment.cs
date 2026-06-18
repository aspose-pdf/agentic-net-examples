using System;
using System.IO;
using Aspose.Pdf;

namespace ValidatePdfAAfterZugferdAttachment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF document (self‑contained example)
            using (Document createDoc = new Document())
            {
                // Aspose.Pdf uses 1‑based page indexing; add the first page
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Step 2: Prepare a dummy ZUGFeRD XML file to embed
            string zugferdXmlPath = "zugferd.xml";
            File.WriteAllText(zugferdXmlPath, "<Invoice></Invoice>");

            // Step 3: Open the PDF, embed the ZUGFeRD file, convert to PDF/A‑1b and save
            using (Document doc = new Document("input.pdf"))
            {
                // Create a file specification for the attachment using the constructor that accepts path and description
                FileSpecification fileSpec = new FileSpecification(zugferdXmlPath, "ZUGFeRD Invoice");
                fileSpec.Description = "ZUGFeRD Invoice";

                // Add the specification to the EmbeddedFiles collection (max 4 elements allowed in evaluation mode)
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert the document to PDF/A‑1b compliance (in‑place conversion)
                doc.Convert("conversion.log", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the compliant PDF
                doc.Save("output.pdf");
            }

            // Step 4: Validate PDF/A compliance using the compliance checker
            using (Document validatedDoc = new Document("output.pdf"))
            {
                // The IsPdfACompliant property does not exist; use Validate method to check compliance
                bool validationResult = validatedDoc.Validate("validation.log", PdfFormat.PDF_A_1B);

                Console.WriteLine("Validation result (true = compliant): " + validationResult);
                Console.WriteLine("Validation log written to validation.log");
            }
        }
    }
}
