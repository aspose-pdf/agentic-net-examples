using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string validationLog = "validation.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form object exists (creates one if missing)
            Form form = doc.Form;

            // NOTE: DefaultAppearance class may not exist in some Aspose.Pdf versions.
            // The line below is guarded with reflection to keep compilation safe.
            SetDefaultAppearanceIfAvailable(form);

            // -------------------------
            // Add a text box field (strong‑typed – this class is stable across versions)
            // -------------------------
            Rectangle txtRect = new Rectangle(100, 600, 300, 630);
            TextBoxField txtField = new TextBoxField(doc, txtRect)
            {
                PartialName = "CustomerName",
                AlternateName = "Enter your full name",
                Required = true,
                ReadOnly = false,
                MaxLen = 50,
                Value = ""
            };
            form.Add(txtField, "CustomerName", 1);

            // -------------------------
            // Add a check box field (created via reflection / dynamic to avoid version‑specific compile errors)
            // -------------------------
            Rectangle chkRect = new Rectangle(100, 560, 115, 575);
            dynamic chkField = CreateFormField("Aspose.Pdf.Forms.CheckBoxField", doc, chkRect);
            chkField.PartialName = "SubscribeNewsletter";
            chkField.AlternateName = "Subscribe to newsletter";
            SetPropertyIfExists(chkField, "Checked", true);
            chkField.Required = false;
            form.Add(chkField, "SubscribeNewsletter", 1);

            // -------------------------
            // Add a radio button group with two options (using the newer RadioButtonOptionField class via reflection)
            // -------------------------
            // First option (Male)
            Rectangle radRect1 = new Rectangle(100, 520, 115, 535);
            dynamic radFieldMale = CreateFormField("Aspose.Pdf.Forms.RadioButtonOptionField", doc, radRect1);
            radFieldMale.PartialName = "Gender";               // group name
            radFieldMale.AlternateName = "Male";
            SetPropertyIfExists(radFieldMale, "Checked", true); // default selection
            form.Add(radFieldMale, "Gender_Male", 1);

            // Second option (Female)
            Rectangle radRect2 = new Rectangle(150, 520, 165, 535);
            dynamic radFieldFemale = CreateFormField("Aspose.Pdf.Forms.RadioButtonOptionField", doc, radRect2);
            radFieldFemale.PartialName = "Gender";               // same group name
            radFieldFemale.AlternateName = "Female";
            SetPropertyIfExists(radFieldFemale, "Checked", false);
            form.Add(radFieldFemale, "Gender_Female", 1);

            // -------------------------
            // Add a signature field (strong‑typed – class is stable)
            // -------------------------
            Rectangle sigRect = new Rectangle(100, 460, 300, 500);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature",
                AlternateName = "Sign here"
            };
            form.Add(sigField, "Signature", 1);

            // -------------------------
            // Optional: add an additional appearance for the text field on a second page
            // -------------------------
            if (doc.Pages.Count >= 2)
            {
                Rectangle txtRectPage2 = new Rectangle(100, 600, 300, 630);
                form.AddFieldAppearance(txtField, 2, txtRectPage2);
            }

            // -------------------------
            // Validate the document against PDF/A-1B (example)
            // -------------------------
            bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Document validation result: {isValid}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    // Helper: create form field via reflection to avoid compile‑time dependency on version‑specific classes
    private static dynamic CreateFormField(string fullyQualifiedTypeName, Document doc, Rectangle rect)
    {
        // The type is expected to be in the same assembly as Document
        Assembly asm = typeof(Document).Assembly;
        Type fieldType = asm.GetType(fullyQualifiedTypeName);
        if (fieldType == null)
            throw new InvalidOperationException($"Form field type '{fullyQualifiedTypeName}' not found in assembly '{asm.FullName}'.");
        return Activator.CreateInstance(fieldType, doc, rect);
    }

    // Helper: set a property only if it exists on the dynamic object (prevents runtime MissingMemberException)
    private static void SetPropertyIfExists(dynamic obj, string propertyName, object value)
    {
        var type = ((object)obj).GetType();
        PropertyInfo prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, value);
        }
    }

    // Helper: set DefaultAppearance when the class is available (some versions expose it, others do not)
    private static void SetDefaultAppearanceIfAvailable(Form form)
    {
        // The DefaultAppearance class may be missing; guard with reflection.
        Type daType = typeof(Form).Assembly.GetType("Aspose.Pdf.Forms.DefaultAppearance");
        if (daType != null)
        {
            // Constructor: (string fontName, int fontSize, Color color)
            object daInstance = Activator.CreateInstance(daType, "Helvetica", 12, Color.Black);
            PropertyInfo prop = typeof(Form).GetProperty("DefaultAppearance");
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(form, daInstance);
            }
        }
    }
}
