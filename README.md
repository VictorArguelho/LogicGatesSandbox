# Logic Gates Sandbox

A sandbox de circuitos digitais focada na construção e simulação de sistemas lógicos.

O projeto permite criar circuitos utilizando portas lógicas, conectá-las livremente e experimentar com sistemas digitais cada vez mais complexos — desde circuitos simples até arquiteturas capazes de implementar operações e componentes de um computador.

## Objetivo

O objetivo do projeto é proporcionar uma experiência de construção livre de circuitos digitais, permitindo que o jogador monte, teste e explore sistemas lógicos sem limitações práticas de componentes.

Entre as possibilidades estão:

- Construção livre de circuitos.
- Portas lógicas como AND, OR e NOT.
- Portas derivadas como NAND, NOR, XOR e XNOR.
- Conexão de componentes através de fios.
- Criação de circuitos complexos a partir de componentes simples.
- Construção de sistemas digitais e computadores dentro da sandbox.

## Engine

O projeto está sendo desenvolvido utilizando uma engine própria em **C#**, construída especificamente para as necessidades do Logic Gates Sandbox.

A versão anterior do projeto foi desenvolvida utilizando Unity. A atual versão está sendo reescrita com foco em:

- Alto desempenho.
- Grande quantidade de componentes simultâneos.
- Simulação eficiente de circuitos.
- Renderização em larga escala.
- Baixo overhead por componente.
- Arquitetura especializada para circuitos digitais.

### Tecnologias

- C#
- .NET
- Silk.NET
- OpenGL

## Arquitetura

A engine não pretende reproduzir a arquitetura tradicional de engines genéricas.

O objetivo é que a simulação e a renderização sejam tratadas de maneira mais eficiente, especialmente em circuitos de grande escala.

## Estado do projeto

**Em desenvolvimento**

A versão atual está em processo de reescrita da antiga implementação em Unity para uma engine própria em C#.

## Repositório

A branch `main` contém a versão anterior do projeto desenvolvida em Unity.

A branch `engine-rewrite` contém a nova implementação baseada na engine própria.
