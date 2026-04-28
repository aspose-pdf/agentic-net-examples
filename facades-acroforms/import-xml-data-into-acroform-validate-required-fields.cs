using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";          // Source PDF with AcroForm
        const string xmlPath = "data.xml";          // XML containing field data
        const string outputPath = "filled_form.pdf"; // Resulting PDF after import

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Missing input PDF or XML file.");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize the Form facade on the loaded document
            Form form = new Form(pdfDoc);

            // Import XML data into the form fields
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream); // Uses Form.ImportXml(Stream)
            }

            // Validate that all required fields have non‑empty values
            foreach (string fieldName in form.FieldNames)
            {
                // Determine if the field is marked as required
                if (form.IsRequiredField(fieldName))
                {
                    // Retrieve the field value; may be null or non‑string
                    object rawValue = form.GetField(fieldName);
                    string value = rawValue as string;

                    // Log a violation if the required field is empty or whitespace
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine($"Required field '{fieldName}' is empty.");
                    }
                }
            }

            // Save the updated PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine("Form import and validation completed.");
    }
}