using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF with form fields and the output PDF
        const string inputPath  = "input_form.pdf";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form fields
        using (Document doc = new Document(inputPath))
        {
            // ----- TextBoxField (strong‑typed, stable) -----
            if (doc.Form["CustomerName"] is TextBoxField txtField)
            {
                txtField.Value = "John Doe";
            }

            // ----- CheckBoxField (runtime inspection) -----
            var chkWidget = doc.Form["SubscribeNewsletter"];
            if (chkWidget != null && chkWidget.GetType().Name == "CheckBoxField")
            {
                // Use dynamic to avoid compile‑time dependency on the class name
                dynamic chk = chkWidget;
                chk.Checked = true;

                // ExportValue may not exist in every version – set it only if the property is present
                if (HasProperty(chk, "ExportValue"))
                {
                    chk.ExportValue = "Yes";
                }
            }

            // ----- RadioButtonField (runtime inspection) -----
            var radWidget = doc.Form["Gender"];
            if (radWidget != null && radWidget.GetType().Name == "RadioButtonField")
            {
                dynamic rad = radWidget;
                // RadioButtonField uses 1‑based indexing for the selected option.
                rad.Selected = 2; // selects the second option in the group
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields updated and saved to '{outputPath}'.");
    }

    // Helper that checks for the existence of a property via reflection
    private static bool HasProperty(object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName) != null;
    }
}
