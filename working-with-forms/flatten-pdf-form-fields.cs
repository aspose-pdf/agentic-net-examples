using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // OPTIONAL: fill form fields before flattening
            if (doc.Form != null)
            {
                // Example field names – adjust to your PDF
                Field? nameField = doc.Form["Name"] as Field;
                if (nameField != null)
                    nameField.Value = "John Doe";

                Field? dateField = doc.Form["Date"] as Field;
                if (dateField != null)
                    dateField.Value = DateTime.Today.ToShortDateString();
            }

            // Flatten the form – fields become part of the page content and are no longer editable
            doc.Form.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
