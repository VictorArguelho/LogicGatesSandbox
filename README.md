# Logic Gates Sandbox

Um jogo 2D sandbox feito em **C# com Unity**, focado em lógica digital e no funcionamento básico dos computadores.

O objetivo do jogo é proporcionar um ambiente livre para **aprender, experimentar e construir circuitos lógicos**, permitindo que o jogador compreenda na prática como portas lógicas podem ser combinadas para processar sinais e formar sistemas mais complexos.

## Objetivo

O **Logic Gates Sandbox** busca tornar o aprendizado de lógica digital mais prático e visual.

Em vez de apenas estudar tabelas-verdade e circuitos de forma teórica, o jogador pode colocar componentes em um espaço livre, conectá-los e observar diretamente como os sinais percorrem o circuito.

A ideia é começar com componentes simples e, a partir deles, permitir a construção de sistemas cada vez mais complexos.

## Features

### Portas lógicas

O jogo possui as três portas lógicas fundamentais:

* **AND**
* **OR**
* **NOT**

Além delas, também estão disponíveis portas derivadas:

* **XOR**
* **NAND**
* **NOR**
* **XNOR**

As portas recebem sinais de entrada, processam esses sinais de acordo com sua operação lógica e produzem um sinal de saída.

### Conectores

Um **conector** permite transmitir um sinal para diferentes partes do circuito.

Ele possui uma entrada e pode possuir diversas saídas, sendo que todas as saídas possuem o mesmo valor da entrada.

Também existe o **conector ativável**, que permite definir manualmente o valor do sinal sem precisar de uma entrada.

### Cabos

Os componentes podem ser conectados utilizando **cabos**, permitindo criar circuitos e transportar sinais entre diferentes partes da construção.

Isso possibilita combinar diversas portas e conectores para formar circuitos maiores.

### Sandbox

O jogador possui liberdade para construir seus circuitos em um espaço de trabalho sem uma estrutura de fases tradicional.

É possível:

* Adicionar componentes.
* Posicionar componentes livremente.
* Mover componentes seguindo a grade.
* Selecionar componentes.
* Selecionar múltiplos componentes.
* Excluir componentes.
* Conectar componentes.
* Testar diferentes combinações de portas lógicas.

### Grade

O espaço de construção possui uma **grade infinita**, que auxilia no posicionamento e alinhamento dos componentes.

A escala da grade se adapta conforme o zoom da câmera, permitindo trabalhar tanto com circuitos pequenos quanto com construções maiores.

### Sistema de seleção

Os componentes possuem um sistema de seleção visual que permite identificar quais objetos estão sendo manipulados.

A seleção também serve como base para diferentes interações com os componentes do circuito.

### Ferramentas

O jogo possui diferentes ferramentas para realizar ações específicas no ambiente.

Atualmente, entre elas estão:

* **Seleção**
* **Conexão de cabos**

### Interface de componentes

O jogador pode navegar por um menu de componentes organizado por categorias.

Cada componente possui:

* Imagem.
* Nome.
* Descrição.
* Categoria.

Também é possível abrir uma janela de informações para consultar a função de cada componente antes de utilizá-lo.

## Conceitos abordados

O jogo é baseado em conceitos fundamentais de **lógica digital** e **computação**, como:

* Lógica booleana.
* Valores binários.
* Portas lógicas.
* Tabelas-verdade.
* Sinais digitais.
* Entrada e saída.
* Combinação de operações lógicas.
* Construção de circuitos digitais.

A partir desses conceitos, circuitos maiores podem ser construídos utilizando componentes relativamente simples.

## Construção de circuitos

A principal proposta do jogo é permitir que o jogador experimente livremente com os componentes.

Por exemplo, diferentes portas podem ser combinadas para criar circuitos capazes de realizar operações mais complexas.

Dessa forma, componentes simples podem ser utilizados como blocos de construção para sistemas maiores, aproximando o jogador da forma como circuitos digitais reais são construídos.

## Controles

| Ação                               | Controle                                    |
| ---------------------------------- | ------------------------------------------- |
| Selecionar                         | Botão esquerdo do mouse                     |
| Mover componente                   | Segurar o botão direito do mouse e arrastar |
| Excluir componente                 | `Delete` / `Backspace`                      |
| Alternar ferramenta de seleção     | `1`                                         |
| Alternar ferramenta de cabos       | `2`                                         |
| Ativar/desativar conector ativável | `E`                                         |
| Criar componente selecionado       | `Space`                                     |

> Os controles podem sofrer alterações durante o desenvolvimento.

## Tecnologias

* **Unity**
* **C#**

## Status

O **Logic Gates Sandbox** está atualmente em desenvolvimento.

Novos componentes, ferramentas, sistemas de interação, tutoriais, documentações e possibilidades de construção serão adicionados conforme o projeto evolui.

## Objetivo futuro

A intenção é permitir que o jogador vá além de simplesmente testar portas lógicas e consiga construir **circuitos digitais cada vez mais complexos**, utilizando os componentes básicos como blocos de construção.

A longo prazo, o sandbox pode permitir a construção de sistemas que vão desde circuitos simples até estruturas capazes de representar partes fundamentais de um computador.
