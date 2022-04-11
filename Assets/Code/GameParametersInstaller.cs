using Code.Configs;
using UnityEngine;
using Zenject;

public class GameParametersInstaller : MonoInstaller
{
    [SerializeField] private GameCfg _cfg;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private bool _fromCfg;
    private IGameCfg _gameCfg;
    public override void InstallBindings()
    {
        _gameCfg = _fromCfg?_cfg:_gameConfig;
        Container.Bind<float>().WithId("health").FromInstance(_gameCfg.PlayerHealth);
        Container.Bind<float>().WithId("speed").FromInstance(_gameCfg.PlayerSpeed);
        Container.Bind<float>().WithId("shootDelay").FromInstance(_gameCfg.ShootDelay);
        Container.Bind<float>().WithId("jerkDistance").FromInstance(_gameCfg.JerkDistance);
        Container.Bind<float>().WithId("jerkSpeed").FromInstance(_gameCfg.JerkSpeed);
        Container.Bind<float>().WithId("jerkRechargeTime").FromInstance(_gameCfg.JerkRechargeTime);
    }
}