namespace _Dev.Game.Scripts.EventSystem
{
    public enum EventId
    {
        #region Game
        on_game_initialized = 0,
        #endregion

        #region InGame
        on_grid_right_click = 10,
        on_grid_left_click = 11,
        on_cursor_direction_changed = 12,
        on_production_product_clicked = 13,
        on_product_die = 14,
        #endregion
    }
}