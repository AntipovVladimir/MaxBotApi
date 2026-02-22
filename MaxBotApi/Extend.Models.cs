using MaxBotApi.Models;

namespace MaxBotApi.Models;

public partial class InlineKeyboard
{
    public InlineKeyboard AddNewRow(params Button[] buttons)
    {
        if (Buttons is not List<List<Button>> keyboard)
            throw new InvalidOperationException("This method works only with a List<List<InlineKeyboardButton>> keyboard");
        keyboard.Add([.. buttons]);
        return this;
    }

    public InlineKeyboard AddButton(string text, string callbackData)
        => AddButton(Button.WithCallbackData(text, callbackData));

    public InlineKeyboard AddButton(Button button)
    {
        if (Buttons is not List<List<Button>> keyboard)
            throw new InvalidOperationException("This method works only with a List<List<InlineKeyboardButton>> keyboard");
        if (keyboard.Count == 0) keyboard.Add([]);
        keyboard[^1].Add(button);
        return this;
    }
}