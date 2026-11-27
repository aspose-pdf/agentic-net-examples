using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Verify that the document contains a form with at least one field
            if (doc.Form == null ||
                doc.Form.Fields == null ||
                !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Enumerate all fields and output submit‑button URLs when present
            foreach (Field field in doc.Form.Fields)
            {
                if (field is ButtonField button &&
                    button.OnActivated is SubmitFormAction submitAction)
                {
                    // SubmitFormAction.Url may be a string or a FileSpecification depending on the library version.
                    // Convert it safely to a string for display.
                    string url = submitAction.Url?.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        Console.WriteLine($"{button.PartialName}: {url}");
                    }
                    else
                    {
                        Console.WriteLine($"{button.PartialName}: (no submit URL)");
                    }
                }
                else
                {
                    // For other fields just display the name with a placeholder
                    string name = (field as ButtonField)?.PartialName ??
                                  (field as TextBoxField)?.PartialName ??
                                  field.PartialName ??
                                  "(unnamed)";
                    Console.WriteLine($"{name}: (no submit URL)");
                }
            }
        }
    }
}
