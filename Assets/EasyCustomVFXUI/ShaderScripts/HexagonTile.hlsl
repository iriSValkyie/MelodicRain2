/*
Code copyright (c) 2018 rngtm.
Code released under the MIT license.
*/

void HexagonTile_float(
        float2 UV,
        float Scale,
        out float Hexagon, // �Z�p�`
        out float2 HexPos, // �Z�p�`�̈ʒu
        out float2 HexUV, // �Z�p�`����UV���o��
        out float2 HexIndex // �Z�p�`�̔ԍ�
)

{
    float2 p = UV * Scale;
    p.x *= 1.15470053838;
    float isTwo = frac(floor(p.x) / 2.0) * 2.0; // ������ڂȂ�1.0
    float isOne = 1.0 - isTwo; // ���ڂȂ�1.0
    p.y += isTwo * 0.5; // ������ڂ�0.5���炷
    float2 rectUV = frac(p); // �l�p�`�^�C��
    float2 grid = floor(p); // �l�p�`�O���b�h
    p = frac(p) - 0.5;
    float2 s = sign(p); // �}�X�ڂ̉E��:(+1, +1) ����:(-1, +1) �E��:(+1, -1) ����:(-1, -1)
    p = abs(p); // �㉺���E�Ώ̂ɂ���
        // �Z�p�`�^�C���Ƃ��ďo��
    Hexagon = abs(max(p.x * 1.5 + p.y, p.y * 2.0) - 1.0);
    float isInHex = step(p.x * 1.5 + p.y, 1.0); // �Z�p�`�̓����Ȃ�1.0
    float isOutHex = 1.0 - isInHex; // �Z�p�`�̊O���Ȃ�1.0
        // �l�p�`�}�X�̂����A�Z�p�`�̊O���̕�����␳���邽�߂Ɏg�p����l
    float2 grid2 = float2(0, 0);
        // ������ڂƊ��ڂ𓯎��ɉ��H
    grid2 = lerp(
            float2(s.x, +step(0.0, s.y)), // ���� (isTwo=0.0�̏ꍇ�͂�������̗p)
            float2(s.x, -step(s.y, 0.0)), // ������� (isTwo=1.0�̏ꍇ�͂�������̗p)
            isTwo) * isOutHex; // �Z�p�`�̊O���������o��
        // �Z�p�`�̔ԍ��Ƃ��ďo��
    HexIndex = grid + grid2;
        // �Z�p�`�̍��W�Ƃ��ďo��
    HexPos = HexIndex / Scale;
        // �Z�p�`�̓����Ȃ�rectUV�A�O���Ȃ�4�̘Z�p�`��UV���g��
    HexUV = lerp(rectUV, rectUV - s * float2(1.0, 0.5), isOutHex);
}