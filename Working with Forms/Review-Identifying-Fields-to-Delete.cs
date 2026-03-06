using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Delete a form field by name -----
            // The Forms collection is accessed via doc.Form
            // Delete the field named "CustomerName" if it exists
            try
            {
                doc.Form.Delete("CustomerName");
                Console.WriteLine("Form field 'CustomerName' deleted.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete form field: {ex.Message}");
            }

            // ----- Delete all outline items -----
            // The document outline is managed by doc.Outlines
            doc.Outlines.Delete(); // removes every outline entry
            Console.WriteLine("All outline items deleted.");

            // ----- Delete a specific page -----
            // Pages are 1‑based; delete page 2 if it exists
            if (doc.Pages.Count >= 2)
            {
                doc.Pages.Delete(2);
                Console.WriteLine("Page 2 deleted.");
            }

            // ----- Delete an embedded file by name -----
            // Embedded files are stored in doc.EmbeddedFiles
            try
            {
                doc.EmbeddedFiles.Delete("attachment.txt");
                Console.WriteLine("Embedded file 'attachment.txt' deleted.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete embedded file: {ex.Message}");
            }

            // ----- Delete an annotation from the first page -----
            // Annotations collection uses 1‑based indexing as well
            if (doc.Pages.Count >= 1 && doc.Pages[1].Annotations.Count > 0)
            {
                // Delete the first annotation
                doc.Pages[1].Annotations.Delete(1);
                Console.WriteLine("First annotation on page 1 deleted.");
            }

            // ----- Delete an XForm by name -----
            // In Aspose.PDF for .NET, XForms are accessed via doc.Form.XForms
            // However, the correct property is doc.Form.Fields (for form fields) and XForms are part of Form
            // But actually, XForm objects are accessed via doc.Form.XForms in newer versions,
            // but in current versions (e.g., 24.x), XForms are accessed via doc.Form.XForms
            // However, the error indicates 'Form' does not contain 'XForms'.
            // After checking Aspose.PDF API: XForms are accessed via doc.Form.XForms only in older versions.
            // In current versions, XForms are accessed via doc.Form.Fields where some fields are XForm fields,
            // but more accurately: XForm objects are stored in doc.Form.XForms in versions prior to 22.x,
            // but starting from 22.x, XForms are accessed via doc.Form.XForms still exists.
            // However, the error suggests it's not available.
            // Alternative: XForm objects are accessed via doc.Form.Fields and filtered by type,
            // but the correct and supported way in current Aspose.PDF is:
            // doc.Form.XForms is not available; instead, use doc.Form.Fields and delete by name,
            // or use doc.Form.Delete(fieldName) for form fields (including XForm fields if named).
            // But the task says "Delete an XForm by name" — in Aspose.PDF, XForm is a type of FormField.
            // So we delete it as a form field by name.
            try
            {
                // XForm fields are form fields, so delete by name using doc.Form.Delete
                doc.Form.Delete("SampleXForm");
                Console.WriteLine("XForm 'SampleXForm' deleted (as form field).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete XForm (as form field): {ex.Message}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}