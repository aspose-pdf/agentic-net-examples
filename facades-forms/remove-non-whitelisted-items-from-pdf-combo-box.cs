using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the temporary input PDF and the final output PDF.
        const string inputPdf = "FormWithState.pdf";
        const string outputPdf = "FormStateCleaned.pdf";
        const string listFieldName = "State";

        // Whitelist of items that should remain in the list field.
        var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Alabama",
            "Alaska",
            "Arizona"
        };

        // All items that will initially be present in the list field.
        var allItems = new[]
        {
            "Alabama",
            "Alaska",
            "Arizona",
            "Arkansas",
            "California",
            "Colorado"
        };

        // ---------------------------------------------------------------------
        // 1. Create a self‑contained PDF that contains a combo (drop‑down) field named "State"
        //    and populate it with the sample items.
        // ---------------------------------------------------------------------
        Document seedDoc = new Document();
        Page page = seedDoc.Pages.Add();

        // Create a rectangle that defines the field position and size.
        // (llx, lly) = lower‑left corner, (urx, ury) = upper‑right corner.
        var rect = new Rectangle(100, 600, 200, 650);

        // Use ComboBoxField (drop‑down) – the constructor takes the page and rectangle.
        ComboBoxField stateField = new ComboBoxField(page, rect);
        stateField.PartialName = listFieldName;

        // Populate the combo box with the initial items using AddOption.
        foreach (var itm in allItems)
        {
            stateField.AddOption(itm);
        }

        // Add the field to the document's form collection.
        seedDoc.Form.Add(stateField);
        // Save the seed PDF so that FormEditor can work with a real file.
        seedDoc.Save(inputPdf);

        // ---------------------------------------------------------------------
        // 2. Open the PDF with FormEditor (use the non‑obsolete constructor).
        // ---------------------------------------------------------------------
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // ---------------------------------------------------------------------
        // 3. Remove items that are not in the whitelist.
        // ---------------------------------------------------------------------
        foreach (var item in allItems)
        {
            if (!whitelist.Contains(item))
            {
                formEditor.DelListItem(listFieldName, item);
            }
        }

        // ---------------------------------------------------------------------
        // 4. Save the cleaned PDF.
        // ---------------------------------------------------------------------
        formEditor.Save(outputPdf);

        Console.WriteLine($"List items cleaned. Output saved to '{outputPdf}'.");
    }
}
