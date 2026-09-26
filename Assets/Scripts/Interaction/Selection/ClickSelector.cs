using System.Collections.Generic;

public static class ClickSelector
{
    public static bool Click(List<SelectableTarget> selectables)
    {
        var hasBeenClicked = false;

        foreach (var selectable in selectables)
        {
            selectable.Deselect();

            if (!hasBeenClicked)
                hasBeenClicked = selectable.TrySelect();
        }

        return hasBeenClicked;
    }
}