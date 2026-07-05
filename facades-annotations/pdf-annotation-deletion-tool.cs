using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace AsposePdfAnnotationDeletionTool
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnnotationDeletionForm());
        }
    }

    public class AnnotationDeletionForm : Form
    {
        private Button btnOpen;
        private Button btnDelete;
        private Button btnSave;
        private CheckedListBox clbTypes;
        private Label lblStatus;
        private string currentPdfPath;
        private Document pdfDocument;

        public AnnotationDeletionForm()
        {
            // Form properties
            this.Text = "Aspose.PDF Annotation Deletion Tool";
            this.Width = 380;
            this.Height = 340;
            this.StartPosition = FormStartPosition.CenterScreen;

            // UI setup
            btnOpen = new Button { Text = "Open PDF", Left = 10, Top = 10, Width = 100 };
            btnDelete = new Button { Text = "Delete Selected", Left = 120, Top = 10, Width = 120 };
            btnSave = new Button { Text = "Save PDF", Left = 250, Top = 10, Width = 100 };
            clbTypes = new CheckedListBox { Left = 10, Top = 50, Width = 340, Height = 200 };
            lblStatus = new Label { Left = 10, Top = 260, Width = 340, Height = 30 };

            // Add controls to form
            this.Controls.AddRange(new Control[] { btnOpen, btnDelete, btnSave, clbTypes, lblStatus });

            // Wire events
            btnOpen.Click += BtnOpen_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentPdfPath = ofd.FileName;
                        pdfDocument = new Document(currentPdfPath);
                        PopulateAnnotationTypes();
                        lblStatus.Text = $"Loaded '{Path.GetFileName(currentPdfPath)}' – {pdfDocument.Pages.Count} page(s).";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PopulateAnnotationTypes()
        {
            clbTypes.Items.Clear();
            clbTypes.CheckedItems.Clear();
            HashSet<AnnotationType> typesFound = new HashSet<AnnotationType>();

            foreach (Page page in pdfDocument.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot != null && typesFound.Add(annot.AnnotationType))
                    {
                        // Add the type to the list; initially unchecked.
                        clbTypes.Items.Add(annot.AnnotationType);
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (pdfDocument == null)
            {
                MessageBox.Show("Please open a PDF first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedTypes = clbTypes.CheckedItems.Cast<AnnotationType>().ToList();
            if (!selectedTypes.Any())
            {
                MessageBox.Show("Select at least one annotation type to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int removedCount = 0;
            foreach (Page page in pdfDocument.Pages)
            {
                // Collect annotations to remove to avoid modifying collection while iterating
                var toRemove = page.Annotations
                                   .Where(a => selectedTypes.Contains(a.AnnotationType))
                                   .ToList();
                foreach (var annot in toRemove)
                {
                    page.Annotations.Delete(annot);
                    removedCount++;
                }
            }

            lblStatus.Text = $"Deleted {removedCount} annotation(s). Click 'Save PDF' to persist changes.";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (pdfDocument == null)
            {
                MessageBox.Show("No PDF loaded to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                sfd.FileName = Path.GetFileNameWithoutExtension(currentPdfPath) + "_modified.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pdfDocument.Save(sfd.FileName);
                        lblStatus.Text = $"PDF saved to '{Path.GetFileName(sfd.FileName)}'.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
} // end of AsposePdfAnnotationDeletionTool namespace

// ---------------------------------------------------------------------------
// Minimal WinForms stubs – added to make the project compile when the real
// System.Windows.Forms assembly is not referenced. They provide just enough
// members for the code above to build; at runtime they act as no‑op UI.
// ---------------------------------------------------------------------------
namespace System.Windows.Forms
{
    public enum FormStartPosition { CenterScreen }
    public enum DialogResult { OK, Cancel }
    public enum MessageBoxButtons { OK, OKCancel, YesNo, YesNoCancel }
    public enum MessageBoxIcon { None, Information, Warning, Error }

    public static class Application
    {
        public static void EnableVisualStyles() { }
        public static void SetCompatibleTextRenderingDefault(bool defaultValue) { }
        public static void Run(Form mainForm) { /* No UI – just keep the process alive */ }
    }

    public class Form : IDisposable
    {
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public FormStartPosition StartPosition { get; set; }
        public Control.ControlCollection Controls { get; } = new Control.ControlCollection();
        public void Dispose() { }
    }

    public class Control
    {
        public string Text { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public event EventHandler Click;
        public void PerformClick() => Click?.Invoke(this, EventArgs.Empty);

        public class ControlCollection : List<Control>
        {
            public void AddRange(IEnumerable<Control> controls) => base.AddRange(controls);
            public void Add(params Control[] controls) => base.AddRange(controls);
        }
    }

    public class Button : Control { }
    public class Label : Control { }
    public class CheckedListBox : Control
    {
        public CheckedListBox()
        {
            Items = new List<object>();
            CheckedItems = new List<object>();
        }
        public List<object> Items { get; }
        public List<object> CheckedItems { get; }
        // Helper to mimic the real Add method with a checked flag
        public void AddItem(object item, bool isChecked)
        {
            Items.Add(item);
            if (isChecked) CheckedItems.Add(item);
        }
    }
    public class OpenFileDialog : IDisposable
    {
        public string Filter { get; set; }
        public string FileName { get; set; }
        public DialogResult ShowDialog() => DialogResult.OK;
        public void Dispose() { }
    }
    public class SaveFileDialog : IDisposable
    {
        public string Filter { get; set; }
        public string FileName { get; set; }
        public DialogResult ShowDialog() => DialogResult.OK;
        public void Dispose() { }
    }
    public static class MessageBox
    {
        public static void Show(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None) { }
    }
}
