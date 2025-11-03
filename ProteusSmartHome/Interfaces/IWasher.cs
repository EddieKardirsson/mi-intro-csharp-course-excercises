namespace ProteusSmartHome.Interfaces;

public interface IWasher
{
    public double StartProgram(byte program);
    public void Wash();
    public void Rinse();
}