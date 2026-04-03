using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form_input.pdf";
        const string outputPath = "form_filled_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, process form fields, then set metadata
        using (Document doc = new Document(inputPath))
        {
            // Example form processing: set values for known fields
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Set a text field named "Name"
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // Set a checkbox named "Subscribe"
                // Use the generic Field type to avoid a direct dependency on CheckBoxField (which may not be present in some versions)
                if (doc.Form["Subscribe"] is Field subscribeField)
                {
                    // For a checkbox the logical way is to set its value to "On" (checked) or "Off" (unchecked)
                    subscribeField.Value = "On"; // or "Off" depending on the desired state
                }

                // Optional: flatten the form to make fields non‑editable
                // doc.Form.Flatten();
            }

            // Set PDF metadata
            doc.Info.Author = "Acme Corp.";
            doc.Info.Title = "Completed Form Document";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
    }
}
