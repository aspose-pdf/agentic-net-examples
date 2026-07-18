using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // will be created on‑the‑fly
        const string outputPdf = "output.pdf"; // result after updating the field
        const string fieldName = "CountryList"; // name of the dropdown (combo box) field

        // -----------------------------------------------------------------
        // 1. Create a minimal PDF that contains the required form field.
        //    This guarantees the file exists in the sandbox and the field is
        //    present for the subsequent FormEditor operations.
        // -----------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page.
            Page page = seed.Pages.Add();

            // Create a combo box (dropdown) field named "CountryList".
            // The rectangle defines the visual position of the field.
            ComboBoxField combo = new ComboBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 600, 200, 620) // <-- correct ctor
            );
            combo.PartialName = fieldName;
            // No items are added yet – we will clear (there are none) and add new ones later.
            seed.Form.Add(combo);

            // Save the seed PDF so that FormEditor can bind to it.
            seed.Save(inputPdf);
        }

        // -----------------------------------------------------------------
        // 2. New items that should appear in the dropdown after clearing the old ones.
        // -----------------------------------------------------------------
        string[] newItems = new[] { "USA", "Canada", "Mexico", "Germany", "France" };

        // -----------------------------------------------------------------
        // 3. Bind the PDF with FormEditor (Facades API) and update the field.
        // -----------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 4. OPTIONAL: Remove any existing items.
            //    Since we just created the field it is empty, but the pattern is
            //    shown for completeness – iterate over current options and delete them.
            // -----------------------------------------------------------------
            using (Document tmpDoc = new Document(inputPdf))
            {
                var field = tmpDoc.Form[fieldName] as ComboBoxField;
                if (field != null)
                {
                    // field.Options is the correct collection (not Items).
                    foreach (var opt in field.Options)
                    {
                        // DelListItem removes an entry by its *value*.
                        formEditor.DelListItem(fieldName, opt.Value);
                    }
                }
            }

            // -----------------------------------------------------------------
            // 5. Add the new items to the dropdown field.
            // -----------------------------------------------------------------
            foreach (string item in newItems)
            {
                formEditor.AddListItem(fieldName, item);
            }

            // -----------------------------------------------------------------
            // 6. Save the updated PDF.
            // -----------------------------------------------------------------
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Dropdown field \"{fieldName}\" updated and saved to '{outputPdf}'.");
    }
}
