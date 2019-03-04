
public interface IBuffable
{
    void ApplyBuffs();
    void AddBuff(Buff buff);

    bool BuffExists<T>() where T : Buff;
}

