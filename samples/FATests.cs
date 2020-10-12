using Godot;

namespace FontAwesome
{
    public class FATests : Control
    {
        private LineEdit _lineEdit;
        private IconTouchButton _customIcon;

        public override void _Ready()
        {
            _lineEdit = GetNode<LineEdit>("Row/RightCol/TopCol/Row/LineEdit");
            _customIcon = GetNode<IconTouchButton>("Row/RightCol/TopCol/Icon");
            _lineEdit.Connect("text_changed", this, nameof(UpdateCustomIcon));
        }

        private void UpdateCustomIcon(string name)
        {
            _customIcon.Text = name;
        }
    }
}
