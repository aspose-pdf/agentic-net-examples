using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageAcroForm.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add three pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= 3; i++)
            {
                doc.Pages.Add();
            }

            // ---------- Page 1: TextBox and CheckBox ----------
            {
                // TextBox field on page 1
                TextBoxField txt = new TextBoxField(doc.Pages[1], new Rectangle(100, 700, 300, 730));
                txt.PartialName = "Page1_TextBox";
                txt.Value = "Sample text";
                doc.Form.Add(txt);

                // CheckBox field on page 1
                CheckboxField chk = new CheckboxField(doc.Pages[1], new Rectangle(100, 650, 120, 670));
                chk.PartialName = "Page1_CheckBox";
                chk.Checked = true;
                doc.Form.Add(chk);
            }

            // ---------- Page 2: RadioButton group and ListBox ----------
            {
                // RadioButton group on page 2
                RadioButtonField radio = new RadioButtonField(doc.Pages[2]);
                radio.PartialName = "Page2_RadioGroup";
                // Add two options – each option needs its own rectangle
                radio.AddOption("Option A", new Rectangle(100, 700, 120, 720));
                radio.AddOption("Option B", new Rectangle(100, 680, 120, 700));
                radio.Selected = 1; // 1‑based index, selects "Option A"
                doc.Form.Add(radio);

                // ListBox field on page 2
                ListBoxField list = new ListBoxField(doc.Pages[2], new Rectangle(100, 600, 200, 650));
                list.PartialName = "Page2_ListBox";
                list.AddOption("Item 1");
                list.AddOption("Item 2");
                list.AddOption("Item 3");
                doc.Form.Add(list);
            }

            // ---------- Page 3: Signature and ComboBox ----------
            {
                // Signature field on page 3 (empty placeholder)
                SignatureField sig = new SignatureField(doc.Pages[3], new Rectangle(100, 700, 300, 750));
                sig.PartialName = "Page3_Signature";
                doc.Form.Add(sig);

                // ComboBox field on page 3
                ComboBoxField combo = new ComboBoxField(doc.Pages[3], new Rectangle(100, 600, 200, 630));
                combo.PartialName = "Page3_ComboBox";
                combo.AddOption("Choice 1");
                combo.AddOption("Choice 2");
                // Optionally set a default selection (1‑based)
                combo.Selected = 1;
                doc.Form.Add(combo);
            }

            // Save the PDF containing distinct AcroForm fields per page
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
