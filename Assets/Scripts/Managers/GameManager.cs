using System;
using Extentions;
using Enums;
using Keys;
using Signals;

public class GameManager : MonoSingleton<GameManager>
{
    #region Self Variables

    #region Public Variables

    public GameStates States;

    #endregion

    #endregion
    private void OnEnable()
    {
        SubscribeEvents();
    }
    void SubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameStates;
    } 
    void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameStates;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void OnChangeGameStates(GameStates newStates)
    {
        States = newStates;
    }
    private void OnSaveGame(SaveGameDataParams saveGameDataParams)
    {
        ES3.Save("Level",saveGameDataParams.Level);
        ES3.Save("Coins",saveGameDataParams.Coins);
        ES3.Save("SFX",saveGameDataParams.SFX);
        ES3.Save("VFX",saveGameDataParams.VFX);
        ES3.Save("haptic",saveGameDataParams.haptic);
    }
}
