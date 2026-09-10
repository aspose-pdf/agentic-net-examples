using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath = "formData.xfdf";
        const string outputPdfPath = "target_synced.pdf";

        // ------------------------------------------------------------
        // 1. Create a source PDF that contains a form field with a value.
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document())
        {
            Page page = sourceDoc.Pages.Add();
            // Create a text box field named "NameField" and set an initial value.
            TextBoxField sourceField = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "NameField",
                Value = "John Doe"
            };
            sourceDoc.Form.Add(sourceField);
            sourceDoc.Save(sourcePdfPath);
        }

        // ------------------------------------------------------------
        // 2. Create a target PDF that has the same field name but no value.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document())
        {
            Page page = targetDoc.Pages.Add();
            TextBoxField targetField = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "NameField",
                Value = "" // empty value – will be filled from XFDF
            };
            targetDoc.Form.Add(targetField);
            targetDoc.Save(targetPdfPath);
        }

        // ------------------------------------------------------------
        // 3. Export the form data (as XFDF) from the source PDF.
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // 4. Import the XFDF data into the target PDF and save the result.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadFields(xfdfStream, targetDoc);
            }
            targetDoc.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 5. Clean‑up temporary XFDF file.
        // ------------------------------------------------------------
        if (File.Exists(xfdfPath))
        {
            File.Delete(xfdfPath);
        }

        Console.WriteLine($"Form data synchronized. Output saved to '{outputPdfPath}'.");
    }
}
