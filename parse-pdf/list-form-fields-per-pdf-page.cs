using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Length == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                Console.WriteLine($"Page {page.Number}:");

                // Iterate all form fields and select those that belong to the current page
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    // Field.PageIndex is zero‑based; convert to 1‑based for comparison
                    if (field.PageIndex + 1 == page.Number)
                    {
                        Console.WriteLine($"  Field Name : {field.FullName}");
                        Console.WriteLine($"  Field Type : {field.GetType().Name}");

                        // Retrieve field‑specific values using pattern matching
                        switch (field)
                        {
                            case TextBoxField txt:
                                Console.WriteLine($"  Value      : {txt.Value}");
                                break;
                            case CheckboxField chk:
                                Console.WriteLine($"  Checked    : {chk.Checked}");
                                break;
                            case RadioButtonField rad:
                                // Selected is a 1‑based index of the chosen option
                                Console.WriteLine($"  Selected   : {rad.Selected}");
                                break;
                            // Add handling for other field types as needed
                        }
                    }
                }
            }
        }
    }
}
