namespace _Dev.Game.Scripts.EventSystem
{
    public enum EventId
    {
        #region Game
        on_game_initialized = 0,
        on_game_start = 1,
        on_game_end = 2,
        on_game_pause = 3,
        on_game_resume = 4,
        on_game_reset = 5,
        #endregion

        #region InGame
        on_grid_right_click = 10,
        on_grid_left_click = 11,
        on_cursor_direction_changed = 12,
        #endregion
    }
}