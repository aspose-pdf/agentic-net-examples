using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // AnnotationFlags, TextAlignment (if needed)
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string headerImagePath = "header_bg.png";
        const string headerFieldName = "Header";
        const string headerText = "My Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1️⃣  Determine the rectangle of the "Header" field (or fall back to the
        //     whole first page if the field does not exist).
        // ---------------------------------------------------------------------
        Document tempDoc = new Document(inputPath);
        Aspose.Pdf.Rectangle fieldRect = null;
        foreach (var field in tempDoc.Form)
        {
            if (field.FullName.Equals(headerFieldName, StringComparison.OrdinalIgnoreCase))
            {
                fieldRect = field.Rect;
                break;
            }
        }
        if (fieldRect == null)
        {
            Page firstPage = tempDoc.Pages[1];
            fieldRect = new Aspose.Pdf.Rectangle(
                firstPage.PageInfo.Margin.Left,
                firstPage.PageInfo.Margin.Bottom,
                firstPage.PageInfo.Width - firstPage.PageInfo.Margin.Right,
                firstPage.PageInfo.Height - firstPage.PageInfo.Margin.Top);
        }

        // ---------------------------------------------------------------------
        // 2️⃣  Add the background image as an ImageStamp that exactly covers the
        //     field rectangle.  ImageStamp works on a per‑page basis and is the
        //     recommended way to place a bitmap behind a form field.
        // ---------------------------------------------------------------------
        // Assume the field lives on page 1 – if you need the exact page, you can
        // retrieve it from the field's PageNumber property (available in newer
        // versions). For the purpose of this example page 1 is sufficient.
        Page targetPage = tempDoc.Pages[1];
        ImageStamp bgStamp = new ImageStamp(headerImagePath)
        {
            Width = fieldRect.URX - fieldRect.LLX,
            Height = fieldRect.URY - fieldRect.LLY,
            LeftMargin = fieldRect.LLX,
            BottomMargin = fieldRect.LLY,
            // Keep the stamp behind the field content – stamps are added in the
            // order they are called, so we add it *before* we modify the field.
        };
        targetPage.AddStamp(bgStamp);

        // Save the intermediate PDF (with the image stamp) to a temporary file.
        const string intermediatePath = "intermediate.pdf";
        tempDoc.Save(intermediatePath);

        // ---------------------------------------------------------------------
        // 3️⃣  Set the field's text alignment to Center using FormEditor.
        //     The API expects an *int* that corresponds to Aspose.Pdf.HorizontalAlignment.
        // ---------------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(intermediatePath);
            editor.SetFieldAlignment(headerFieldName, (int)Aspose.Pdf.HorizontalAlignment.Center);
            editor.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 4️⃣  Fill the field with the desired text.  FormEditor does NOT set field
        //     values – the Facade class Aspose.Pdf.Facades.Form is used for that.
        // ---------------------------------------------------------------------
        using (Form form = new Form())
        {
            form.BindPdf(outputPath);
            form.FillField(headerFieldName, headerText);
            form.Save(outputPath);
        }

        // Clean up the intermediate file.
        if (File.Exists(intermediatePath))
            File.Delete(intermediatePath);

        Console.WriteLine($"Header field decorated and saved to '{outputPath}'.");
    }
}
